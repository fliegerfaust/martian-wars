using UnityEngine;

namespace Code.Infrastructure.Services.Input
{
  public interface IInputService
  {
    Vector2 JoystickAxis { get; }
    Vector2 TouchpadAxis { get; }
    bool IsAttackButtonUp();
  }
}