using System;
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
      _cinemachineFreeLook.Follow = target;
      _cinemachineFreeLook.LookAt = target;
    }
  }
}