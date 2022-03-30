using JetBrains.Annotations;
using Vector2 = UnityEngine.Vector2;

namespace Code.Infrastructure.Services.Input
{
  [UsedImplicitly]
  public class MobileInputService : InputService
  {
    public override Vector2 JoystickAxis => SimpleInputAxis();
    public override Vector2 TouchpadAxis => SimpleInputTouchpadAxis();
  }
}