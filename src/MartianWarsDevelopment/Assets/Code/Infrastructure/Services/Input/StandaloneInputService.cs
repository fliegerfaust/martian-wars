using JetBrains.Annotations;
using UnityEngine;

namespace Code.Infrastructure.Services.Input
{
  [UsedImplicitly]
  public class StandaloneInputService : InputService
  {
    public override Vector2 JoystickAxis
    {
      get
      {
        Vector2 axis = SimpleInputAxis();

        if (axis == Vector2.zero)
          axis = UnityAxis();

        return axis;
      }
    }

    public override Vector2 TouchpadAxis
    {
      get
      {
        Vector2 axis = SimpleInputTouchpadAxis();

        if (axis == Vector2.zero)
          axis = UnityMouseAxis();

        return axis;
      }
    }

    private static Vector2 UnityAxis() =>
      new Vector2(UnityEngine.Input.GetAxis(Horizontal), UnityEngine.Input.GetAxis(Vertical));

    private static Vector2 UnityMouseAxis() =>
      new Vector2(UnityEngine.Input.GetAxis(MouseX), UnityEngine.Input.GetAxis(MouseY));
  }
}