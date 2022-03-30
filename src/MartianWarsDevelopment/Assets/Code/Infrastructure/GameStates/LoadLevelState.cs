using Code.CameraLogic;
using Code.Infrastructure.Services.Factory;
using Code.Infrastructure.Services.Input;
using Code.Infrastructure.Services.StaticData;
using Code.Player;
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
    private readonly IInputService _inputService;

    public LoadLevelState
    (
      GameStateMachine stateMachine,
      SceneLoader sceneLoader,
      IStaticDataService staticData,
      IPlayerFactory playerFactory,
      IInputService inputService
    )
    {
      _stateMachine = stateMachine;
      _sceneLoader = sceneLoader;
      _staticData = staticData;
      _playerFactory = playerFactory;
      _inputService = inputService;
    }

    public void Enter(string sceneName)
    {
      _staticData.Load();
      _sceneLoader.Load(sceneName, OnLoaded);
    }

    public void Exit()
    {
    }

    private void OnLoaded()
    {
      InitGameWorld();

      _stateMachine.Enter<TestState>();
    }

    private void InitGameWorld()
    {
      LevelStaticData levelData = LevelStaticData();
      GameObject player = InitPlayer(levelData);

      InitHud();
      InitInputService(player);
      CameraFollow(player);
    }

    private GameObject InitPlayer(LevelStaticData levelData) =>
      _playerFactory.CreatePlayer(levelData.PlayerInitialPosition);

    private void InitHud() =>
      _playerFactory.CreateHud();

    private void InitInputService(GameObject player) =>
      player.GetComponent<PlayerDrive>().Construct(_inputService);

    private void CameraFollow(GameObject player) =>
      Camera.main.GetComponent<CameraFollow>().Follow(player);

    private LevelStaticData LevelStaticData() =>
      _staticData.ForLevel(SceneManager.GetActiveScene().name);
  }
}