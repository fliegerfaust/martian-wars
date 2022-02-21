using UnityEngine;

namespace Code.Infrastructure.GameStates
{
  public class LoadLevelState : IPayloadedState<string>
  {
    private readonly GameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;

    public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader)
    {
      _stateMachine = stateMachine;
      _sceneLoader = sceneLoader;
    }
    
    public void Enter(string sceneName)
    {
      _sceneLoader.Load(sceneName, OnLoaded);
    }

    private void OnLoaded()
    {
      Debug.Log("Demo scene has been loaded!");
      _stateMachine.Enter<TestState>();
    }

    public void Exit()
    {
      
    }
  }
}