using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class AttractTo : MonoBehaviour {
	public Transform _attractedTo;
	public float _strengthOfAttraction, _maxMagnitude;
	Rigidbody _rigidbody;


	void Awake () {
		_rigidbody = GetComponent<Rigidbody>();
	}
	
	void FixedUpdate () {
		if (_attractedTo != null) {
			Vector3 direction = _attractedTo.position - transform.position;
			_rigidbody.AddForce (_strengthOfAttraction * direction);

			if (_rigidbody.velocity.magnitude > _maxMagnitude) {
				_rigidbody.velocity = _rigidbody.velocity.normalized * _maxMagnitude;
			}
		}
	}
}