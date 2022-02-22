using Code.Infrastructure.AssetManagement;
using UnityEngine;

namespace Code.Infrastructure.Services.Factory
{
  public class PlayerFactory : IPlayerFactory
  {
    private IAssets _assets;

    public PlayerFactory(IAssets assets)
    {
      _assets = assets;
    }

    public GameObject CreatePlayer(Vector3 at) => 
      _assets.Instantiate(AssetPath.PlayerPath, at);

    public GameObject CreateHud() =>
      _assets.Instantiate(AssetPath.HudPath);
  }
}