using UnityEngine;
using UnityEngine.SceneManagement;

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
      Debug.Log($"Active scene: {SceneManager.GetActiveScene().name}");
    }
  }
}