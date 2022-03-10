using UnityEngine;

namespace Code.CameraLogic
{
  public class CameraFollow : MonoBehaviour
  {
    private Transform _target;
    [SerializeField] private Vector3 _offset;

    [Range(0, 10)] [SerializeField] private float _lerpPositionMultiplier = 1f;
    [Range(0, 10)] [SerializeField] private float _lerpRotationMultiplier = 1f;

    private Rigidbody _rigidbody;

    public void Follow(GameObject target) =>
      _target = target.transform;

    private void Start() =>
      _rigidbody = GetComponent<Rigidbody>();

    private void FixedUpdate()
    {
      if (_target == null) return;

      _rigidbody.velocity.Normalize();

      Quaternion rotation = transform.rotation;
      Vector3 position = _target.position + _target.TransformDirection(_offset);

      transform.LookAt(_target);

      if (position.y < _target.position.y)
        position.y = _target.position.y;

      transform.SetPositionAndRotation(
        Vector3.Lerp(transform.position, position, Time.fixedDeltaTime * _lerpPositionMultiplier),
        Quaternion.Lerp(rotation, transform.rotation, Time.fixedDeltaTime * _lerpRotationMultiplier)
      );

      if (transform.position.y < 0.5f)
        transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);
    }
  }
}