using System;
using UnityEngine;

namespace Code.Player
{
  public class PlayerDrive : MonoBehaviour
  {
    private const string ThrottleInput = "Vertical";
    private const string TurnInput = "Horizontal";

    [SerializeField]
    private AnimationCurve _turnInputCurve = AnimationCurve.Linear(-1.0f, -1.0f, 1.0f, 1.0f);

    [Header("Wheels")]
    
    [SerializeField]
    private WheelCollider[] _driveWheels = Array.Empty<WheelCollider>();
    [SerializeField]
    private WheelCollider[] _turnWheels = Array.Empty<WheelCollider>();

    [Header("Behaviour")] [SerializeField]
    
    private AnimationCurve _motorTorque =
      new AnimationCurve(new Keyframe(0, 200), new Keyframe(50, 300), new Keyframe(200, 0));

    [Range(2, 16)] [SerializeField]
    private float _diffGearing = 4.0f;
    [SerializeField]
    private float _brakeForce = 1500.0f;
    [Range(0.001f, 1.0f)] [SerializeField]
    private float _steerSpeed = 0.2f;
    [Range(0f, 50.0f)] [SerializeField]
    private float _steerAngle = 30.0f;
    [SerializeField]
    private Transform _centerOfMass;
    [Range(0.5f, 10f)] [SerializeField]
    private float _downforce = 1.0f;
    [SerializeField]
    private float _speed;

    private float _steering;
    private float _throttle;
    private Rigidbody _rigidbody;
    private WheelCollider[] _wheelColliders = Array.Empty<WheelCollider>();
    
    private void Start()
    {
      _rigidbody = GetComponent<Rigidbody>();
      _wheelColliders = GetComponentsInChildren<WheelCollider>();
      
      InitializeRigidbodyAndCenterOfMass();
      UpdateWheelColliders();
    }

    private void FixedUpdate()
    {
      GetAccelerateAndBreakInput();
      SetSteeringDirection();
      UpdateWheelColliders();
      MeasureCurrentSpeed();
      ApplyHandbrake();
      ApplyDownforce();
    }

    private void GetAccelerateAndBreakInput()
    {
      if (!string.IsNullOrEmpty(ThrottleInput))
        _throttle = GetInput(ThrottleInput);
    }

    private void SetSteeringDirection()
    {
      _steering = _turnInputCurve.Evaluate(GetInput(TurnInput)) * _steerAngle;
      foreach (WheelCollider wheel in _turnWheels)
        wheel.steerAngle = Mathf.Lerp(wheel.steerAngle, _steering, _steerSpeed);
    }

    private void UpdateWheelColliders()
    {
      foreach (WheelCollider wheel in _wheelColliders)
      {
        wheel.motorTorque = 0.0001f;
        wheel.brakeTorque = 0;
      }
    }

    private void MeasureCurrentSpeed() =>
      _speed = transform.InverseTransformDirection(_rigidbody.velocity).z * 3.6f;

    private void ApplyHandbrake()
    {
      if (_throttle != 0 && (Mathf.Abs(_speed) < 4 || Math.Abs(Mathf.Sign(_speed) - Mathf.Sign(_throttle)) < 0.0001f))
        foreach (WheelCollider wheel in _driveWheels)
          wheel.motorTorque = _throttle * _motorTorque.Evaluate(_speed) * _diffGearing / _driveWheels.Length;
      else if (_throttle != 0)
        foreach (WheelCollider wheel in _wheelColliders)
          wheel.brakeTorque = Mathf.Abs(_throttle) * _brakeForce;
    }

    private void ApplyDownforce() =>
      _rigidbody.AddForce(-transform.up * (_speed * _downforce));

    private void InitializeRigidbodyAndCenterOfMass()
    {
      if (_rigidbody != null && _centerOfMass != null)
        _rigidbody.centerOfMass = _centerOfMass.localPosition;
    }

    // Use this method if you want to use your own input manager
    private float GetInput(string input) =>
      Input.GetAxis(input);
  }
}