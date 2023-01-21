using FMODUnity;
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

	float lastChargeValue = 0;
	public void SetBatteryLevel(float value, float maxValue)
	{
		batteryLevelSlider.maxValue = maxValue;
		lastChargeValue = value / maxValue;
		var result = Helpers.NearestMultipleOfNumber(lastChargeValue, .125f);
		//Debug.Log(result * 10);
		batteryLevelSlider.value = result * 10;
		if(result < .13f) batteryLevelFill.color = Color.red;
		else if (result < .8f) batteryLevelFill.color = Color.yellow;
		else batteryLevelFill.color = Color.white;
	}
	[SerializeField] float flashSpeed = 3;
	[SerializeField] StudioEventEmitter chargingEmitter;
	IEnumerator FlashChargeImage()
	{
		batteryLevelFill.color = Helpers.ChangeColorAlpha(batteryLevelFill.color, 1);
		bool fadeDown = true;
		float timeElapsed = 0;
		chargingEmitter.SetParameter("chargeLoudness", 1);
		chargingEmitter.Play();
		
		while(chargeDisplayed)
		{
			timeElapsed += Time.deltaTime;

			float alpha = 0;
			if (fadeDown) alpha = Mathf.Clamp01(1 - (timeElapsed / flashSpeed));
			else alpha = Mathf.Clamp01(timeElapsed / flashSpeed);

			batteryLevelFill.color = Helpers.ChangeColorAlpha(batteryLevelFill.color, alpha);
			if (timeElapsed >= flashSpeed)
			{
				chargingEmitter.Params[0].Value = 1 - lastChargeValue;
				chargingEmitter.Play();
				timeElapsed = 0;
				fadeDown = !fadeDown;
			}
			
			yield return null;
		}
		batteryLevelFill.color = Helpers.ChangeColorAlpha(batteryLevelFill.color, 1);
	}

	[SerializeField] GameObject deathPanel;
	[SerializeField] Image deathPanelImage;
	[SerializeField] TextMeshProUGUI deathPoemText;
	[SerializeField] Button[] deathButtons;
	[SerializeField] TextMeshProUGUI[] deathButtonTexts;
	public void ShowDeathScreen()
	{
		StartCoroutine(DeathScreenSequence());
	}

	IEnumerator DeathScreenSequence()
	{
		deathPanelImage.color = Helpers.ChangeColorAlpha(deathPanelImage.color, 0);
		deathPoemText.color = Helpers.ChangeColorAlpha(deathPoemText.color, 0);
		foreach (var text in deathButtonTexts) text.color = Helpers.ChangeColorAlpha(text.color, 0);
		foreach (var button in deathButtons) button.enabled = false;
		List<Image> buttonImages = new List<Image>();
		foreach (var button in deathButtons) buttonImages.Add(button.GetComponent<Image>());
		foreach (var image in buttonImages) image.color = Helpers.ChangeColorAlpha(image.color, 0);
		deathPanel.SetActive(true);
		
		float timeElapsed = 0;
		float fadeUpTime = 1f;
		// fade up death panel BG image
		while (timeElapsed < fadeUpTime)
		{
			timeElapsed += Time.deltaTime;
			deathPanelImage.color = Helpers.ChangeColorAlpha(deathPanelImage.color, timeElapsed / fadeUpTime);
			yield return null;
		}
		// fade up poem
		timeElapsed = 0;
		while (timeElapsed < fadeUpTime)
		{
			timeElapsed += Time.deltaTime;
			deathPoemText.color = Helpers.ChangeColorAlpha(deathPoemText.color, timeElapsed / fadeUpTime);
			yield return null;
		}
		// fade up button texts
		timeElapsed = 0;
		while (timeElapsed < fadeUpTime)
		{
			timeElapsed += Time.deltaTime;
			foreach (var text in deathButtonTexts) text.color = Helpers.ChangeColorAlpha(text.color, timeElapsed/fadeUpTime);
			foreach (var image in buttonImages) image.color = Helpers.ChangeColorAlpha(image.color, (timeElapsed / fadeUpTime) / 2);
			yield return null;
		}
		foreach (var button in deathButtons) button.enabled = true;
		
	}

	public void TryAgain()
	{
		if (GameManager.Instance) GameManager.Instance.ReloadSceneFromLast();
	}

	public void QuitApp()
	{
		if (GameManager.Instance) GameManager.Instance.QuitApp();
		
	}
	
}
