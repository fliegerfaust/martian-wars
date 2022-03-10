using JetBrains.Annotations;
using Zenject;

namespace Code.Infrastructure.GameStates
{
  [UsedImplicitly]
  public class GameStateMachineInitializer : IInitializable
  {
    private const string DemoSceneName = "Demo";

    private readonly GameStateMachine _stateMachine;
    private readonly LoadLevelState _loadLevelState;
    private readonly TestState _testState;

    public GameStateMachineInitializer
    (
      GameStateMachine stateMachine,
      LoadLevelState loadLevelState,
      TestState testState
    )
    {
      _stateMachine = stateMachine;
      _loadLevelState = loadLevelState;
      _testState = testState;
    }

    public void Initialize()
    {
      RegisterStates();
      EnterLoadLevel();
    }

    private void RegisterStates()
    {
      _stateMachine.RegisterState(_testState);
      _stateMachine.RegisterState(_loadLevelState);
    }

    private void EnterLoadLevel() =>
      _stateMachine.Enter<LoadLevelState, string>(DemoSceneName);
  }
}