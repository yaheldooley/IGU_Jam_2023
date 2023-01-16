using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
//using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
	[SerializeField] AudioSource _source;
	[SerializeField] CCMover _mover;
	[SerializeField] Interactor _interactor;

	[SerializeField] CinemachineInputProvider _cinemachineLookInput;
	private void Start()
	{
		SetControlState(ControlState.Character);
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}
	public enum ControlState
	{
		Character,
		Interactor
	}

	ControlState _currentState = ControlState.Interactor;
	public void SetControlState(ControlState state)
	{
		if (state == _currentState) return;
		switch(state)
		{
			case ControlState.Character:
				_interactor.enabled = false;
				_cinemachineLookInput.enabled = true;
				_mover.enabled = true;
				break;

			case ControlState.Interactor:
				_mover.enabled = false;
				_cinemachineLookInput.enabled = false;
				_interactor.enabled = true;
				break;
		}
	}

	private void OnEnable()
	{

	}

	private void OnTriggerEnter(Collider other)
	{

	}

	private void OnTriggerExit(Collider other)
	{
		
	}


}
