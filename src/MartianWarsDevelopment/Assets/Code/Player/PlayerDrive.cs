using System;
using Code.Infrastructure.Services.Input;
using UnityEngine;
using Zenject;

namespace Code.Player
{
  public class PlayerDrive : MonoBehaviour
  {
    private const float ToKmHMultiplier = 3.6f;
    private const int HandbrakeApplySpeed = 4;

    [SerializeField] private AnimationCurve _turnInputCurve = AnimationCurve.Linear(-1.0f, -1.0f, 1.0f, 1.0f);

    [Header("Wheels")] [SerializeField] private WheelCollider[] _driveWheels = Array.Empty<WheelCollider>();
    [SerializeField] private WheelCollider[] _turnWheels = Array.Empty<WheelCollider>();

    [Header("Behaviour")] [SerializeField] private AnimationCurve _motorTorque =
      new AnimationCurve(new Keyframe(0, 200), new Keyframe(50, 300), new Keyframe(200, 0));

    [Range(2, 16)] [SerializeField] private float _diffGearing = 4.0f;
    [SerializeField] private float _brakeForce = 1500.0f;
    [Range(0.001f, 1.0f)] [SerializeField] private float _steerSpeed = 0.2f;
    [Range(0f, 50.0f)] [SerializeField] private float _steerAngle = 30.0f;
    [SerializeField] private Transform _centerOfMass;
    [Range(0.5f, 10f)] [SerializeField] private float _downforce = 1.0f;
    [SerializeField] private float _speed;

    private float _steeringInput;
    private float _throttleInput;
    private Rigidbody _rigidbody;
    private WheelCollider[] _wheelColliders = Array.Empty<WheelCollider>();

    private IInputService _inputService;

    [Inject]
    public void Construct(IInputService inputService) =>
      _inputService = inputService;

    private void Start()
    {
      _rigidbody = GetComponent<Rigidbody>();
      _wheelColliders = GetComponentsInChildren<WheelCollider>();

      InitializeRigidbodyAndCenterOfMass();
      UpdateWheelColliders();
    }

    private void Update() =>
      GetInput();

    private void FixedUpdate()
    {
      SetSteeringDirection();
      UpdateWheelColliders();
      MeasureCurrentSpeed();
      ApplyThrottleAndBrake();
      ApplyDownforce();
    }

    private void GetInput()
    {
      _throttleInput = _inputService.JoystickAxis.y;
      _steeringInput = _turnInputCurve.Evaluate(_inputService.JoystickAxis.x) * _steerAngle;
    }

    private void SetSteeringDirection()
    {
      foreach (WheelCollider wheel in _turnWheels)
        wheel.steerAngle = Mathf.Lerp(wheel.steerAngle, _steeringInput, _steerSpeed);
    }

    private void UpdateWheelColliders()
    {
      foreach (WheelCollider wheel in _wheelColliders)
      {
        wheel.motorTorque = Constants.Epsilon;
        wheel.brakeTorque = 0;
      }
    }

    private void MeasureCurrentSpeed() =>
      _speed = transform.InverseTransformDirection(_rigidbody.velocity).z * ToKmHMultiplier;

    private void ApplyThrottleAndBrake()
    {
      if (_throttleInput != 0 && (Mathf.Abs(_speed) < HandbrakeApplySpeed ||
                                  Mathf.Abs(Mathf.Sign(_speed) - Mathf.Sign(_throttleInput)) < Mathf.Epsilon))
        foreach (WheelCollider wheel in _driveWheels)
          wheel.motorTorque = _throttleInput * _motorTorque.Evaluate(_speed) * _diffGearing / _driveWheels.Length;
      else if (_throttleInput != 0)
        foreach (WheelCollider wheel in _wheelColliders)
          wheel.brakeTorque = Mathf.Abs(_throttleInput) * _brakeForce;
    }

    private void ApplyDownforce() =>
      _rigidbody.AddForce(-transform.up * (_speed * _downforce));

    private void InitializeRigidbodyAndCenterOfMass()
    {
      if (_rigidbody != null && _centerOfMass != null)
        _rigidbody.centerOfMass = _centerOfMass.localPosition;
    }
  }
}