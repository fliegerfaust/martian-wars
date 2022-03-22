using Cinemachine;
using UnityEngine;

namespace Code.CameraLogic
{
  public class CinemachineCameraFollow : MonoBehaviour
  {
    private CinemachineVirtualCamera _cinemachineFreeLook;

    public void Follow(Transform target)
    {
      _cinemachineFreeLook = GetComponent<CinemachineVirtualCamera>();
      Transform followPoint = target.GetChild(0).transform;

      _cinemachineFreeLook.Follow = followPoint;
      _cinemachineFreeLook.LookAt = followPoint;
    }
  }
}