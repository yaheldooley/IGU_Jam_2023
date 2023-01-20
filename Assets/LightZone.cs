using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightZone : MonoBehaviour
{
    public float chargeRate;
    public bool HasPower = true;
	public float Radius = 0;

	[SerializeField] Light light;

	

	private void Start()
	{
		Radius = GetComponent<SphereCollider>().radius;
		var overlapped = Physics.OverlapSphere(transform.position, Radius);
		bool inView = false;
		foreach(var overlaps in overlapped)
		{
			if (overlaps.gameObject.CompareTag("LightSwitcher")) inView = true;
		}
		light.enabled = HasPower && inView;
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("LightSwitcher"))
		{
			light.enabled = HasPower;
			
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("LightSwitcher"))
		{
			light.enabled = false;
		}
	}

}
