using Zenject;

namespace Code.Infrastructure.GameStates
{
  public class GameStateMachineInitializer : IInitializable
  {
    private const string DemoSceneName = "Demo";
    
    private readonly GameStateMachine _stateMachine;
    private readonly TestState _testState;
    private readonly LoadLevelState _loadLevelState;

    public GameStateMachineInitializer(
      TestState testState,
      GameStateMachine stateMachine,
      LoadLevelState loadLevelState
    )
    {
      _stateMachine = stateMachine;
      _loadLevelState = loadLevelState;
      _testState = testState;
    }

    public void Initialize()
    {
      _stateMachine.RegisterState(_testState);
      _stateMachine.RegisterState(_loadLevelState);

      EnterLoadLevel();
    }

    private void EnterLoadLevel()
    {
      _stateMachine.Enter<LoadLevelState, string>(DemoSceneName);
    }
  }
}