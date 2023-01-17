using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CameraZone : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera cam;
    void Start()
    {
        cam.enabled = false;
    }

	private void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("Player"))
        {
            cam.enabled = true;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			cam.enabled = false;
		}
	}
}
