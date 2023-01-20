using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightZone : MonoBehaviour
{
    public float chargeRate;
    public bool HasPower = true;
	public float Radius = 0;

	[SerializeField] Light _light;
	[SerializeField] UnityEngine.AI.NavMeshObstacle _obstacle;
	[SerializeField] FMODUnity.StudioEventEmitter generatorSfx;
	private void Start()
	{
		Radius = GetComponent<SphereCollider>().radius;
		var overlapped = Physics.OverlapSphere(transform.position, Radius);
		bool inView = false;
		foreach(var overlaps in overlapped)
		{
			if (overlaps.gameObject.CompareTag("LightSwitcher")) inView = true;
		}
		_light.enabled = HasPower && inView;
	}

	public void SwitchOn()
	{
		HasPower = true;
		if (generatorSfx)
		{
			generatorSfx.Play();
			StartCoroutine(WaitForLight());
		}
		else _light.enabled = true;
	}

	private IEnumerator WaitForLight()
	{
		yield return new WaitForSeconds(1);
		_light.enabled = true;
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("LightSwitcher"))
		{
			_light.enabled = HasPower;
			
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("LightSwitcher"))
		{
			_light.enabled = false;
		}
	}

}
