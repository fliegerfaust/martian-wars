using UnityEngine;
using Cinemachine;
using UnityEngine.EventSystems;

namespace Code.Infrastructure.Services.Input
{
  public class TouchInputControls : MonoBehaviour
  {
    [SerializeField] private float _touchSensitivityX = 10f, _touchSensitivityY = 10f;
    [SerializeField] private float _xMin, _xMax, _yMin, _yMax;

    private int _insideAreaTouchId = -1;
    private bool _released = false;
    private Touch _analogTouch;

    private void Start() =>
      CinemachineCore.GetInputAxis = HandleAxisInputDelegate;

    private bool CheckArea(Vector2 pos)
    {
      Vector2 npos = new Vector2(pos.x / Screen.width, pos.y / Screen.height);
      if (npos.x > _xMin && npos.x < _xMax && npos.y > _yMin && npos.y < _yMax)
        return true;

      return false;
    }

    private void Update()
    {
      if (UnityEngine.Input.touchCount > 0)
      {
        if (_released)
          _insideAreaTouchId = GetAnalogTouchIDInsideArea(); //-1 = none

        if (_insideAreaTouchId != -1)
        {
          _analogTouch = UnityEngine.Input.GetTouch(_insideAreaTouchId);
          if (_released)
          {
            if (_analogTouch.phase == TouchPhase.Began)
            {
              _released = false;
              TouchBegan();
            }
          }
          else if (_analogTouch.phase == TouchPhase.Ended) TouchEnd();
        }
        else
          _released = true;
      }
      else
      {
        _insideAreaTouchId = -1;
        _released = true;
      }
    }

    private float HandleAxisInputDelegate(string axisName)
    {
      switch (axisName)
      {
        case "Mouse X":
          if (UnityEngine.Input.touchCount > 0 && _insideAreaTouchId != -1 &&
              !EventSystem.current.IsPointerOverGameObject())
            return UnityEngine.Input.touches[_insideAreaTouchId].deltaPosition.x / _touchSensitivityX;
          else
            return UnityEngine.Input.GetAxis(axisName);
        case "Mouse Y":
          if (UnityEngine.Input.touchCount > 0 && _insideAreaTouchId != -1 &&
              !EventSystem.current.IsPointerOverGameObject())
            return UnityEngine.Input.touches[_insideAreaTouchId].deltaPosition.y / _touchSensitivityY;
          else
            return UnityEngine.Input.GetAxis(axisName);
        default:
          Debug.LogError("Input <" + axisName + "> not recognized.", this);
          break;
      }

      return 0f;
    }

    private int GetAnalogTouchIDInsideArea()
    {
      for (int i = 0; i < UnityEngine.Input.touchCount; i++)
        if (CheckArea(UnityEngine.Input.GetTouch(i).position))
          return i;

      return -1;
    }

    private void TouchBegan() =>
      _released = false;

    private void TouchEnd()
    {
      _released = true;
      _insideAreaTouchId = -1;
    }
  }
}