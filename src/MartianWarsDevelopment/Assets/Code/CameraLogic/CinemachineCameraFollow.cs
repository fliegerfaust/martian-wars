using Cinemachine;
using UnityEngine;

namespace Code.CameraLogic
{
  public class CinemachineCameraFollow : MonoBehaviour
  {
    private CinemachineFreeLook _cinemachineFreeLook;

    public void Follow(Transform target)
    {
      _cinemachineFreeLook = GetComponent<CinemachineFreeLook>();
      Transform followPoint = target.GetChild(0).transform;

      _cinemachineFreeLook.Follow = followPoint;
      _cinemachineFreeLook.LookAt = followPoint;
    }
  }
}