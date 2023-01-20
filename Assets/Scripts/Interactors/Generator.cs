using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour, IInteract
{
	private bool _inUse = false;
	public bool CanUse => !InUse;
	public bool InUse => _inUse;
	[SerializeField] Collider triggerCol;
	[SerializeField] GameObject modelObject;
	public Transform Transform => this.transform;
	[SerializeField] Transform selectorTransform;
	public Transform SelectorTransform { get => selectorTransform; }
	[SerializeField] float selectorSize = 1;
	public float SelectorSize => selectorSize;

	[SerializeField] LightZone lightZone;

	[SerializeField] FMODUnity.StudioEventEmitter fillGasSfx;

	public bool BeginInteract()
	{
		triggerCol.enabled = false;
		_fillTime = 3.8f;
		StartCoroutine(AddGas());
		return true;
	}
	private float _fillTime = 3;
	private IEnumerator AddGas()
	{
		fillGasSfx.Play();
		float elapsedTime = 0;
		while(elapsedTime < _fillTime)
		{
			elapsedTime += Time.deltaTime;
			yield return null;
		}
		_inUse = false;
		lightZone.SwitchOn();
		IInteract.OnInteractionComplete(this);
	}

	public bool CancelInteract()
	{
		if(_inUse)
		{
			StopCoroutine(AddGas());
			return true;
		}
		return true;
	}

	public void InteractorEnter()
	{
		
	}

	public void InteractorExit()
	{
		
	}
}
