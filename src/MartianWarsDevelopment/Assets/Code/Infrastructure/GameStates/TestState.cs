using UnityEngine;

namespace Code.Infrastructure.GameStates
{
  public class TestState : IState
  {
    public void Exit()
    {
      
    }

    public void Enter()
    {
      Debug.Log("Entered test state!");
    }
  }
}