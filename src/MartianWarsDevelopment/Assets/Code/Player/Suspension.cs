using UnityEngine;

namespace Code.Player
{
  [RequireComponent(typeof(WheelCollider))]
  public class Suspension : MonoBehaviour
  {
    public bool _cancelSteerAngle = false;
    public GameObject _wheelModel;
    public Vector3 _localRotOffset;

    private WheelCollider _wheelCollider;
    private float _lastUpdate;

    void Start()
    {
      _lastUpdate = Time.realtimeSinceStartup;
      _wheelCollider = GetComponent<WheelCollider>();
    }

    void FixedUpdate()
    {
      if (Time.realtimeSinceStartup - _lastUpdate < 1f / 60f)
        return;

      _lastUpdate = Time.realtimeSinceStartup;

      if (_wheelModel && _wheelCollider)
      {
        _wheelCollider.GetWorldPose(out Vector3 pos, out Quaternion quaternion);

        _wheelModel.transform.rotation = quaternion;
        if (_cancelSteerAngle)
          _wheelModel.transform.rotation = transform.parent.rotation;

        _wheelModel.transform.localRotation *= Quaternion.Euler(_localRotOffset);
        _wheelModel.transform.position = pos;

        _wheelCollider.GetGroundHit(out WheelHit _);
      }
    }
  }
}