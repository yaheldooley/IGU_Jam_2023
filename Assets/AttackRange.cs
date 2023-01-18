using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AttackRange : MonoBehaviour
{
	public List<Collider> AllInRange = new List<Collider>();

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == LayerMask.NameToLayer("Target") && !AllInRange.Contains(other)) AllInRange.Add(other);
	}
	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.layer == LayerMask.NameToLayer("Target") && AllInRange.Contains(other)) AllInRange.Remove(other);
	}
}
