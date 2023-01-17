using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseInteraction : MonoBehaviour, IInteract
{
	/// <summary>
	/// 
	/// Create a class for each different type interaction and inherit this one
	/// 
	/// </summary>

	protected bool _inUse = false;
	protected bool _used = false;
	public virtual bool CanUse { get { return !_inUse && !_used; } }
	public bool InUse { get => _inUse; }

	public Transform Transform => this.transform;
	[SerializeField] Transform selectorTransform;
	public Transform SelectorTransform => selectorTransform;

	public float SelectorSize => throw new System.NotImplementedException();

	public virtual bool BeginInteract()
	{
		return true;
	}

	public virtual bool CancelInteract()
	{
		return false;
	}

	public virtual void InteractorEnter()
	{
		// Highlight or outline object
	}

	public virtual void InteractorExit()
	{
		// remove highlights/outlines
	}
}
