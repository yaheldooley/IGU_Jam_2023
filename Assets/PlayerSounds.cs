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
		startStepEmitter.Params[0].Value = _mover.MoveSpeedNormalized;
		startStepEmitter.Play();
	}

	public void PlayFootStep()
    {
		
		footstepEmitter.Play();
	}
}
