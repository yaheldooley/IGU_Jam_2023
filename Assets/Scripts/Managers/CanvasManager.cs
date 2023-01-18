using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager Instance;
	private void Awake()
	{
		if(Instance != null)
		{
			Destroy(this.gameObject);
			return;
		}
		Instance = this;
	}

	public TextMeshProUGUI text;
	[SerializeField] Slider batteryLevelSlider;
	[SerializeField] Image batteryLevelFill;
	[SerializeField] Image chargingIcon;

	bool chargeDisplayed = false;
	public void DisplayChargeIsHappening(bool value)
	{
		if (value == chargeDisplayed) return;
		chargeDisplayed = value;
		if (value)
		{
			StopCoroutine(FlashChargeImage());
			StartCoroutine(FlashChargeImage());
		}
		chargingIcon.enabled = chargeDisplayed;
	}

	public void SetBatteryLevel(float value, float maxValue)
	{
		batteryLevelSlider.maxValue = maxValue;
		var result = Helpers.NearestMultipleOfNumber(value / maxValue, .125f);
		//Debug.Log(result * 10);
		batteryLevelSlider.value = result * 10;
		if(result < .13f) batteryLevelFill.color = Color.red;
		else if (result < .8f) batteryLevelFill.color = Color.yellow;
		else batteryLevelFill.color = Color.white;
	}
	[SerializeField] float flashSpeed = 3;
	IEnumerator FlashChargeImage()
	{
		batteryLevelFill.color = Helpers.ChangeColorAlpha(batteryLevelFill.color, 1);
		bool fadeDown = true;
		float timeElapsed = 0;
		while(chargeDisplayed)
		{
			timeElapsed += Time.deltaTime;

			float alpha = 0;
			if (fadeDown) alpha = Mathf.Clamp01(1 - (timeElapsed / flashSpeed));
			else alpha = Mathf.Clamp01(timeElapsed / flashSpeed);

			batteryLevelFill.color = Helpers.ChangeColorAlpha(batteryLevelFill.color, alpha);
			if (timeElapsed >= flashSpeed)
			{
				timeElapsed = 0;
				fadeDown = !fadeDown;
			}
			
			yield return null;
		}
		batteryLevelFill.color = Helpers.ChangeColorAlpha(batteryLevelFill.color, 1);
	}
}
