using Code.Infrastructure.Services.Input;
using UnityEngine;
using Zenject;

namespace Code.Player
{
  public class PlayerCameraRotation : MonoBehaviour
  {
    private float _verticalLook;
    private float _horizontalLook;

    private IInputService _inputService;

    public void Construct(IInputService inputService) =>
      _inputService = inputService;

    private void Update() =>
      GetInput();

    private void GetInput()
    {
      _verticalLook = _inputService.TouchpadAxis.y;
      _horizontalLook = _inputService.TouchpadAxis.x;
      Debug.Log($"Touchpad Axis: {_verticalLook} {_horizontalLook}");
    }
  }
}