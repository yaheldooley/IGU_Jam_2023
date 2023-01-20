using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour, IInteract
{
	bool pickedUp = false;
	public bool CanUse => !pickedUp;

	public bool InUse => false;
	[SerializeField] Collider triggerCol;
	[SerializeField] GameObject modelObject;
	public Transform Transform => this.transform;
	[SerializeField] Transform selectorTransform;
	public Transform SelectorTransform { get => selectorTransform; }
	[SerializeField] float selectorSize = 1;
	public float SelectorSize => selectorSize;

	public bool BeginInteract()
	{
		// Add item to inventory
		// Make this not interactable
		Debug.Log("Picked up");
		pickedUp = true;
		modelObject.SetActive(false);
		triggerCol.enabled = false;
		IInteract.OnInteractionComplete(this);
		return true;
	}

	public bool CancelInteract()
	{
		return true;
	}

	public void InteractorEnter()
	{
		// Highlight
	}

	public void InteractorExit()
	{
		// Remove highlight
	}
}
