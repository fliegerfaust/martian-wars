using Code.Infrastructure.Services.Input;
using UnityEngine;
using Code.Infrastructure.AssetManagement;
using Code.Infrastructure.GameStates;
using Code.Infrastructure.Services.Factory;
using Code.Infrastructure.Services.StaticData;
using Zenject;

namespace Code.Infrastructure.Installers
{
  public class BootstrapInstaller : MonoInstaller, ICoroutineRunner
  {
    public override void InstallBindings()
    {
      BindCoroutineRunner();
      BindSceneLoader();
      BindAssetProvider();
      BindStaticDataService();

      BindPlayerFactory();
      BindInputService();

      BindGameStates();
      BindGameStateMachine();
    }

    private void BindPlayerFactory() =>
      Container.BindInterfacesAndSelfTo<PlayerFactory>().AsSingle();

    private void BindInputService()
    {
      if (Application.isEditor)
        Container.BindInterfacesAndSelfTo<StandaloneInputService>().AsSingle();
      else
        Container.BindInterfacesAndSelfTo<MobileInputService>().AsSingle();
    }

    private void BindStaticDataService() =>
      Container.BindInterfacesAndSelfTo<StaticDataService>().AsSingle();

    private void BindAssetProvider() =>
      Container.BindInterfacesAndSelfTo<AssetProvider>().AsSingle();

    private void BindCoroutineRunner() =>
      Container.Bind<ICoroutineRunner>().FromInstance(this).AsSingle();

    private void BindSceneLoader() =>
      Container.Bind<SceneLoader>().AsSingle();

    private void BindGameStates()
    {
      Container.BindInterfacesAndSelfTo<LoadLevelState>().AsTransient();
      Container.BindInterfacesAndSelfTo<TestState>().AsTransient();
    }

    private void BindGameStateMachine()
    {
      Container.BindInterfacesAndSelfTo<GameStateMachine>().AsSingle();
      Container.BindInterfacesAndSelfTo<GameStateMachineInitializer>().AsSingle();
    }
  }
}