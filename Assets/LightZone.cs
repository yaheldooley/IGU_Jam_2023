using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightZone : MonoBehaviour
{
    public float chargeRate;
    public bool HasPower = true;
	public float Radius = 0;
	private void Start()
	{
		Radius = GetComponent<SphereCollider>().radius;
	}
}
