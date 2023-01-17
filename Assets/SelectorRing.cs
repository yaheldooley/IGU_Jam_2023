using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorRing : MonoBehaviour
{
	[SerializeField] Transform spriteTransform;
	[SerializeField] Transform parentTransform;
	private void Start()
	{
		transform.parent = null;
		spriteTransform.gameObject.SetActive(false);
	}

	private void OnEnable()
	{
		spriteTransform.gameObject.SetActive(true);
	}

	private void OnDisable()
	{
		spriteTransform.gameObject.SetActive(false);
	}

	[SerializeField] float rotationSpeed = 40f;
	void Update()
    {
		var eul = spriteTransform.localEulerAngles;
		eul.y += Time.deltaTime * rotationSpeed;
		spriteTransform.localEulerAngles = eul;
    }

	public void SetRingPosition(IInteract action)
	{
		this.transform.position = action.SelectorTransform.position;
		this.transform.rotation = action.SelectorTransform.rotation;
		spriteTransform.localScale = new Vector3(action.SelectorSize, action.SelectorSize, action.SelectorSize);
	}
}
