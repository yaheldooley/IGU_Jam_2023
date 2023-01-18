using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryHandler : MonoBehaviour
{
	public float batteryMax = 10;
	public float batteryRemaining = 10;
	public float decayRate = .01f;
	public float decayRateWhileMoving = .035f;

	CCMover _mover;

	private void Awake()
	{
		_mover = GetComponent<CCMover>();
	}

	private void Start()
	{
		StartCoroutine(UpdateBatteryLevelOnHUD());
	}
	private void OnEnable()
	{
		StartCoroutine(UpdateBatteryLevelOnHUD());
	}

	private void OnDisable()
	{
		_updateCycle = false;
	}

	private void Update()
	{
		var decayThisFrame = _mover.IsMoving ? -decayRateWhileMoving : -decayRate;
		if (LightZonesAvailable.Count > 0) decayThisFrame = DetermineChargeLevelThisFrame();
		batteryRemaining += Time.deltaTime * decayThisFrame;

		if(!isCharging && decayThisFrame > 0 || isCharging && decayThisFrame < 0)
		{
			isCharging = !isCharging;
			if (CanvasManager.Instance) CanvasManager.Instance.DisplayChargeIsHappening(isCharging);
		}

	}
	bool _updateCycle = false;
	IEnumerator UpdateBatteryLevelOnHUD()
	{
		if (!_updateCycle)
		{
			_updateCycle = true;
			while (_updateCycle)
			{
				if (CanvasManager.Instance) CanvasManager.Instance.SetBatteryLevel(batteryRemaining, batteryMax);
				yield return new WaitForSeconds(.15f);
			}
		}
	}

	private Dictionary<GameObject, LightZone> LightZonesAvailable = new Dictionary<GameObject, LightZone>();
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("LightZone"))
		{
			if (!LightZonesAvailable.ContainsKey(other.gameObject))
			{
				var zone = other.GetComponent<LightZone>();
				if (zone) LightZonesAvailable.Add(other.gameObject, zone);
			}
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("LightZone"))
		{
			if (LightZonesAvailable.ContainsKey(other.gameObject))
			{
				LightZonesAvailable.Remove(other.gameObject);
			}
		}
	}

	private bool isCharging = false;
	private float DetermineChargeLevelThisFrame()
	{
		float charge = 0;
		foreach(KeyValuePair<GameObject, LightZone> pair in LightZonesAvailable)
		{
			if (pair.Value.HasPower)
			{
				var distToLightCenter = Vector3.Distance(pair.Key.transform.position, this.transform.position);
				var chargeScale = Mathf.Clamp01(distToLightCenter / (pair.Value.Radius / 2));
				charge += pair.Value.chargeRate * chargeScale;
			}
		}
		return charge;
	}

}
