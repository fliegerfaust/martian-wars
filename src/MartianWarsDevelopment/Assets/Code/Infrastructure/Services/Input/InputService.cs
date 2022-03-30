using Cinemachine;
using Zenject;
using Vector2 = UnityEngine.Vector2;

namespace Code.Infrastructure.Services.Input
{
  public abstract class InputService : IInputService, IInitializable
  {
    protected const string Horizontal = "Horizontal";
    protected const string Vertical = "Vertical";
    protected const string MouseX = "Mouse X";
    protected const string MouseY = "Mouse Y";
    private const string Fire = "Fire";

    public abstract Vector2 JoystickAxis { get; }
    public abstract Vector2 TouchpadAxis { get; }

    public bool IsAttackButtonUp() =>
      SimpleInput.GetButtonUp(Fire);

    protected static Vector2 SimpleInputAxis() =>
      new Vector2(SimpleInput.GetAxis(Horizontal), SimpleInput.GetAxis(Vertical));

    protected static Vector2 SimpleInputTouchpadAxis() =>
      new Vector2(SimpleInput.GetAxis(MouseX), SimpleInput.GetAxis(MouseY));

    public void Initialize() =>
      CinemachineCore.GetInputAxis = SimpleInput.GetAxis;
  }
}