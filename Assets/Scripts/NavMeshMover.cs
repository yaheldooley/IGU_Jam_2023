using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshMover : MonoBehaviour
{
    [SerializeField] NavMeshAgent _agent;
    [SerializeField] float maxSpeed = 5;
    [SerializeField] float turnSpeed = 6f;
    EnemyAnimator _anim;

    private void Start()
    {
        _anim = GetComponent<EnemyAnimator>();
        _agent.speed = maxSpeed;
        _agent.angularSpeed = turnSpeed;
    }

    private void OnEnable()
    {
        EnableMovement();
    }

    private void OnDisable()
    {
        DisableMovement();
    }

    float _currentSpeed = 0;

    void Update()
    {
        if (!waitingForMove)
        {
            SetModelAngle();
        }
        _currentSpeed = _agent.velocity.magnitude;
        _anim.SetFloat("speed", _currentSpeed / maxSpeed);
    }

	private void SetModelAngle()
	{
        Vector3 movementDir = _agent.velocity + transform.position;
        movementDir.y = transform.position.y;
        Vector3 targetDir = movementDir - transform.position;
        float angle = Vector3.Angle(targetDir, transform.forward);
        _anim.SetModelAngle(angle);
    }

	public void SetDestination(Vector3 target)
    {
        if (!_agent.enabled) _agent.enabled = true;
        _agent.SetDestination(target);
	}

    bool waitingForMove = false;
    IEnumerator WaitForMovement(float time)
    {
        waitingForMove = true;
        float elapsedTime = 0;
        while (elapsedTime < time)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        waitingForMove = false;
    }
    public void EnableMovement()
    {
        _agent.enabled = true;
    }

    public void DisableMovement()
    {
        _agent.enabled = false;
    }

    public void SetMoverPosition(Vector3 pos)
    {
        StartCoroutine(SetPosition(pos));
    }

    IEnumerator SetPosition(Vector3 pos)
    {
        bool initialStatus = _agent.enabled;
        _agent.enabled = false;
        while (_agent.enabled)
        {
            yield return null;
        }
        transform.position = pos;
        _agent.enabled = initialStatus;
    }
}
