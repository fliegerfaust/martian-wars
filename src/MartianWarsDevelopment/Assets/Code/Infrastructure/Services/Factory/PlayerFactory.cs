using Code.Infrastructure.AssetManagement;
using JetBrains.Annotations;
using UnityEngine;

namespace Code.Infrastructure.Services.Factory
{
  [UsedImplicitly]
  public class PlayerFactory : IPlayerFactory
  {
    private readonly IAssets _assets;

    public PlayerFactory(IAssets assets) =>
      _assets = assets;

    public GameObject CreatePlayer(Vector3 at) =>
      _assets.Instantiate(AssetPath.PlayerPath, at);

    public GameObject CreateHud() =>
      _assets.Instantiate(AssetPath.HudPath);

    public GameObject CreateThirdPersonCamera() =>
      _assets.Instantiate(AssetPath.ThirdPersonCameraPath);
  }
}