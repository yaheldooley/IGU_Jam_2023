using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorRing : MonoBehaviour
{
	[SerializeField] Transform spriteTransform;
	[SerializeField] Transform parentTransform;
	private void Start()
	{
		CameraTracker.BrainReference(Camera.main.GetComponent<Cinemachine.CinemachineBrain>());
		transform.parent = null;
		this.enabled = false;
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
		eul.z += Time.deltaTime * rotationSpeed;
		spriteTransform.localEulerAngles = eul;
		var camTrans = CameraTracker.ActiveCameraTransform;
		if(camTrans) parentTransform.LookAt(camTrans);
    }

	public void SetRingPosition(IInteract action)
	{
		this.transform.position = action.SelectorTransform.position;
		this.transform.rotation = action.SelectorTransform.rotation;
		spriteTransform.localScale = new Vector3(action.SelectorSize, action.SelectorSize, action.SelectorSize);

	}
}
