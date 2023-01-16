using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] Transform _target;
    [SerializeField] float _distanceFromTarget = 3;

    Vector3 _currentRotation;
    Vector3 _smoothVelocity = Vector3.zero;

    [SerializeField] float _smoothTime = 0.2f;

    [SerializeField] float rotationPower = 3;
    float _rotX;
    float _rotY;

    float xAngle = 0;
	private void Start()
	{
        xAngle = transform.eulerAngles.x;
        _distanceFromTarget = Vector3.Distance(transform.position, _target.position);
        transform.parent = null;
	}
	public void SetLookValues(InputAction.CallbackContext context)
    {
        _look = context.ReadValue<Vector2>();
    }
    Vector2 _look = Vector2.zero;

    // Update is called once per frame
    void Update()
    {
        _rotX += _look.x;
        _rotY += _look.y;

        //_rotX = Mathf.Clamp(_rotX, _rotationMinMax.x, _rotationMinMax.y);

        Vector3 nextRotation = new Vector3(xAngle, _rotX * rotationPower, 0);

        _currentRotation = Vector3.SmoothDamp(_currentRotation, nextRotation, ref _smoothVelocity, _smoothTime);
        transform.localEulerAngles = _currentRotation;

        transform.position = _target.position - transform.forward * _distanceFromTarget;
    }
}
