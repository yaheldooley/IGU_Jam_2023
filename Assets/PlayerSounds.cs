using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
	[SerializeField] StudioEventEmitter startStepEmitter;
	[SerializeField] StudioEventEmitter footstepEmitter;
	[SerializeField] CCMover _mover;

	public void StartStep()
	{
		var speed = _mover.MoveSpeedNormalized;
		if (speed < .1f) speed = 0;
		startStepEmitter.SetParameter("chargeLoudness", speed);
		startStepEmitter.Play();
	}

	public void PlayFootStep()
    {
		
		footstepEmitter.Play();
	}
}
