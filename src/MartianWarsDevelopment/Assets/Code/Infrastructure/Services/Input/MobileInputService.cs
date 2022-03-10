using JetBrains.Annotations;
using UnityEngine;

namespace Code.Infrastructure.Services.Input
{
  [UsedImplicitly]
  public class MobileInputService : InputService
  {
    public override Vector2 Axis => SimpleInputAxis();
  }
}