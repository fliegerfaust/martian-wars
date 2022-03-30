using Code.Infrastructure.AssetManagement;
using Code.Player;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Services.Factory
{
  [UsedImplicitly]
  public class PlayerFactory : IPlayerFactory
  {
    private readonly IAssets _assets;
    private readonly DiContainer _container;

    public PlayerFactory(DiContainer container, IAssets assets)
    {
      _container = container;
      _assets = assets;
    }

    public GameObject CreatePlayer(Vector3 at)
    {
      GameObject player = _assets.Instantiate(AssetPath.PlayerPath, at);

      _container.Bind<PlayerDrive>().FromInstance(player.GetComponent<PlayerDrive>());
      _container.InjectGameObject(player);

      return player;
    }

    public GameObject CreateHud() =>
      _assets.Instantiate(AssetPath.HudPath);

    public GameObject CreateThirdPersonCamera() =>
      _assets.Instantiate(AssetPath.ThirdPersonCameraPath);
  }
}