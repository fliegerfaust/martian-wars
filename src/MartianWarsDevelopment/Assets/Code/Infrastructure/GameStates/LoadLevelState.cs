using Code.Infrastructure.Services.Factory;
using Code.Infrastructure.Services.StaticData;
using Code.StaticData;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.Infrastructure.GameStates
{
  public class LoadLevelState : IPayloadedState<string>
  {
    private readonly GameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly IStaticDataService _staticData;
    private readonly IPlayerFactory _playerFactory;

    public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, IStaticDataService staticData, IPlayerFactory playerFactory)
    {
      _stateMachine = stateMachine;
      _sceneLoader = sceneLoader;
      _staticData = staticData;
      _playerFactory = playerFactory;
    }
    
    public void Enter(string sceneName)
    {
      _sceneLoader.Load(sceneName, OnLoaded);
    }

    private void OnLoaded()
    {
      Debug.Log("Demo scene has been loaded!");
      InitGameWorld();
      _stateMachine.Enter<TestState>();
    }

    private void InitGameWorld()
    {
      LevelStaticData levelData = LevelStaticData();
      InitHero(levelData);
    }

    private void InitHero(LevelStaticData levelData) => 
      _playerFactory.CreatePlayer(levelData.PlayerInitialPosition);

    public void Exit()
    {
      
    }

    private LevelStaticData LevelStaticData() => 
      _staticData.ForLevel(SceneManager.GetActiveScene().name);
  }
}