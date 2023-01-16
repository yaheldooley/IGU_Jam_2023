using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Interactor : MonoBehaviour
{
	private Dictionary<GameObject, IInteract> interactionsAvailable = new Dictionary<GameObject, IInteract>();
	private void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("Interact"))
		{
			var i = other.GetComponent<IInteract>();
			if (i == null) return;
			interactionsAvailable.Add(other.gameObject, i);
			i.InteractorEnter();
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

	public IInteract DoInteraction()
	{
		var avail = interactionsAvailable.Values.Where(x=> x.CanUse).ToList();
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
