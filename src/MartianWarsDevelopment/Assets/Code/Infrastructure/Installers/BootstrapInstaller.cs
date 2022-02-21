using Code.Infrastructure.GameStates;
using Zenject;

namespace Code.Infrastructure.Installers
{
  public class BootstrapInstaller : MonoInstaller, ICoroutineRunner
  {
    public override void InstallBindings()
    {
      Container.Bind<ICoroutineRunner>().FromInstance(this).AsSingle();
      
      BindSceneLoader();
      BindGameStates();
      BindGameStateMachine();
    }

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