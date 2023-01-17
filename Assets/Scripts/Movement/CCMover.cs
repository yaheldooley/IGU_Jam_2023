using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CCMover : MonoBehaviour, IMover
{
    [SerializeField] CharacterController controller;
    [SerializeField] Transform camTransform;
    [SerializeField] float maxSpeed = 5;
    float gravity = -25f;
    [SerializeField] float turnSpeed = 6f;

    CinemachineBrain _brain;
    PlayerAnimator _anim;
	private void Start()
	{
        _brain = Camera.main.transform.GetComponent<CinemachineBrain>();
        _anim = GetComponent<PlayerAnimator>();
	}

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
    float _velocity = 0;
    //Vector3 _lastMovement = new Vector3(-.5f, 0, -.5f);
    float _currentSpeed = 0;
	void Update()
    {

        Vector3 movementInput = Quaternion.Euler(0,_brain.ActiveVirtualCamera.VirtualCameraGameObject.transform.eulerAngles.y, 0) * new Vector3(_move.x, 0, _move.y);
        Vector3 movementDir = movementInput.normalized;
        float spd = movementDir.magnitude * maxSpeed;
        float newSpeed = Mathf.Lerp(_currentSpeed, spd, Time.deltaTime * 8);
        _anim.SetFloat("speed", newSpeed / maxSpeed);
        _currentSpeed = newSpeed;
        //Debug.Log("Speed = " + spd.ToString());

        if (movementDir != Vector3.zero)
        {
            Quaternion desiredRot = Quaternion.LookRotation(movementDir, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, desiredRot, turnSpeed * Time.deltaTime);
		}
         
        if (controller.isGrounded) _velocity = -.02f;
        else _velocity += gravity * Time.deltaTime;
        movementInput.y = _velocity;
        controller.Move(movementDir * maxSpeed * Time.deltaTime);
        
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
