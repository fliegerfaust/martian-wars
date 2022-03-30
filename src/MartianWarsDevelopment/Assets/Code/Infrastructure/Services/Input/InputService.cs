using UnityEngine;

namespace Code.Infrastructure.Services.Input
{
  public abstract class InputService : IInputService
  {
    protected const string Horizontal = "Horizontal";
    protected const string Vertical = "Vertical";
    private const string Fire = "Fire";

    public abstract Vector2 Axis { get; }

    public bool IsAttackButtonUp() =>
      SimpleInput.GetButtonUp(Fire);

    protected static Vector2 SimpleInputAxis() =>
      new Vector2(SimpleInput.GetAxis(Horizontal), SimpleInput.GetAxis(Vertical));
  }
}