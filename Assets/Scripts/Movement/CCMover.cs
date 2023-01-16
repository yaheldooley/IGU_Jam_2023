using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CCMover : MonoBehaviour, IMover
{
    [SerializeField] CharacterController controller;
    [SerializeField] Transform camTransform;
    [SerializeField] Transform model;
    [SerializeField] Transform orientation;
    [SerializeField] float walkSpeed = 5;
    [SerializeField] float accelSpeed = 5;
    [SerializeField] float decelSpeed = 3;
    float gravity = 25f;
    float turnSpeed = 3f;

    private void OnEnable()
    {
        controller.enabled = true;
    }

    private void OnDisable()
    {
        controller.enabled = false;
    }

    public void SetMoveValues(InputAction.CallbackContext context)
    {
        _move = context.ReadValue<Vector2>();
	}


    Vector2 _move = Vector2.zero;
    Vector3 _velocity = Vector3.zero;
    //Vector3 _lastMovement = new Vector3(-.5f, 0, -.5f);

	void Update()
    {
        Vector3 movementInput = Quaternion.Euler(0,camTransform.eulerAngles.y, 0) * new Vector3(_move.x, 0, _move.y);
        Vector3 movementDir = movementInput.normalized;

        if (movementDir != Vector3.zero)
        {
            Quaternion desiredRot = Quaternion.LookRotation(movementDir, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, desiredRot, turnSpeed * Time.deltaTime);
		}
        controller.Move(movementInput * walkSpeed * Time.deltaTime);
        
    }
    
    

    public void EnableMovement()
    {
        controller.enabled = false;
    }

    public void DisableMovement()
    {
        controller.enabled = true;
    }

    public void SetMoverPosition(Vector3 pos)
    {
        StartCoroutine(SetPosition(pos));
    }

    IEnumerator SetPosition(Vector3 pos)
    {
        bool initialStatus = controller.enabled;
        controller.enabled = false;
        while (controller.enabled)
        {
            yield return null;
        }
        transform.position = pos;
        controller.enabled = initialStatus;
    }
}
