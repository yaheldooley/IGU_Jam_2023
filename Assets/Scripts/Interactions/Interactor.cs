using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Interactor : MonoBehaviour
{
	[SerializeField] SelectorRing selectorRing;
	private PlayerController _controller;

	private void Awake()
	{
		_controller = GetComponent<PlayerController>();
	}

	private void OnEnable()
	{
		IInteract.InteractionComplete += InteractionComplete;
	}

	private void OnDisable()
	{
		IInteract.InteractionComplete -= InteractionComplete;
	}

	private void InteractionComplete(IInteract action)
	{
		if (action == _currentInteraction)
		{
			_currentInteraction = null;
			_controller.SetControlState(PlayerController.ControlState.Character);
			if (interactionsAvailable.ContainsKey(action.Transform.gameObject)) interactionsAvailable.Remove(action.Transform.gameObject);
			HighlightNearestInteraction();
		}
	}

	private IInteract _currentInteraction;
	private IInteract _nearestAvailable;
	private Dictionary<GameObject, IInteract> interactionsAvailable = new Dictionary<GameObject, IInteract>();
	private void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("Interact"))
		{
			var i = other.GetComponent<IInteract>();
			if (i == null) return;
			interactionsAvailable.Add(other.gameObject, i);
			i.InteractorEnter();
			HighlightNearestInteraction();
			//Debug.Log($"Has interact with {other.gameObject.name}");
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Interact"))
		{
			if (interactionsAvailable.ContainsKey(other.gameObject))
			{
				interactionsAvailable[other.gameObject].InteractorExit();
				interactionsAvailable.Remove(other.gameObject);
				//Debug.Log($"Interact removed {other.gameObject.name}");
			}
			HighlightNearestInteraction();
		}
	}

	public bool HasInteractions { 
		get 
		{
			foreach(var i in interactionsAvailable)
			{
				if (i.Value.CanUse)
				{
					return true;
				}
			}
			return false;
		} 
	}

	public void DoInteraction()
	{
		if (_currentInteraction == null && _nearestAvailable != null)
		{
			_controller.SetControlState(PlayerController.ControlState.Interactor);
			_currentInteraction = _nearestAvailable;
			selectorRing.enabled = false;
			_currentInteraction.BeginInteract();
		}
	}
	private void HighlightNearestInteraction()
	{
		_nearestAvailable = GetClosestAvailableAction();
		if (_nearestAvailable != null)
		{
			selectorRing.SetRingPosition(_nearestAvailable);
			selectorRing.enabled = true;
		}
		else
		{
			selectorRing.enabled = false;
		}
	}

	private IInteract GetClosestAvailableAction()
	{
		var avail = interactionsAvailable.Values.Where(x => x.CanUse).ToList();
		int totalAvail = avail.Count();
		if (totalAvail < 1) return null;
		if (totalAvail == 1) return avail[0];
		SortListByClosest(avail);
		return avail[0];
	}

	public void SortListByClosest(List<IInteract> interactions)
	{
		interactions.OrderBy(x => Vector3.Distance(transform.position, x.Transform.position)).ToList();
	}

	
	
}
