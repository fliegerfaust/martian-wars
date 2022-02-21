using UnityEngine;

namespace Code.Infrastructure.Services.Input
{
  public class MobileInputService : InputService
  {
    public override Vector2 Axis => SimpleInputAxis();
  }
}