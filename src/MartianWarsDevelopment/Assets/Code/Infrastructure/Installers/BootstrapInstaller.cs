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
      
      Container.BindInterfacesAndSelfTo<AssetProvider>().AsSingle();
      Container.BindInterfacesAndSelfTo<StaticDataService>().AsSingle();
      Container.BindInterfacesAndSelfTo<PlayerFactory>().AsSingle();
      
      BindGameStates();
      BindGameStateMachine();
    }

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