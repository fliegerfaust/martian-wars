using Code.Infrastructure.Services.Input;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Installers
{
  public class GameLevelInstaller : MonoInstaller
  {
    public override void InstallBindings()
    {
      BindInputService();
    }

    private void BindInputService()
    {
      if (Application.isEditor)
        Container.BindInterfacesAndSelfTo<StandaloneInputService>().AsSingle();
      else
        Container.BindInterfacesAndSelfTo<MobileInputService>().AsSingle();
    }
  }
}