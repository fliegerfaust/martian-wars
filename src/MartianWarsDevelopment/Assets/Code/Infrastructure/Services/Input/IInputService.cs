using UnityEngine;

namespace Code.Infrastructure.Services.Input
{
  public interface IInputService
  {
    Vector2 Axis { get; }
    bool IsAttackButtonUp();
  }
}