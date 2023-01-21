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
	
	private void Start()
	{
		if (GameManager.Instance) GameManager.Instance.player = this;
		SetControlState(ControlState.Character);
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	public void Dead()
	{
		_mover.enabled = false;
		_interactor.enabled = false;
		GetComponent<BatteryHandler>().enabled = false;
	}

	public void Revive()
	{
		
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

				_mover.enabled = true;
				break;

			case ControlState.Interactor:
				_mover.enabled = false;

				_interactor.enabled = true;
				break;
		}
	}

	private void OnEnable()
	{

	}

	


}
