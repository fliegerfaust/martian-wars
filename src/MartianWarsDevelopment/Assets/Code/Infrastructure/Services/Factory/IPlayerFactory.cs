using UnityEngine;

namespace Code.Infrastructure.Services.Factory
{
  public interface IPlayerFactory
  {
    GameObject CreatePlayer(Vector3 at);
    GameObject CreateHud();
    GameObject CreateThirdPersonCamera();
  }
}