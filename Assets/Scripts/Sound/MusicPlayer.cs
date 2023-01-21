using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
public class MusicPlayer : MonoBehaviour
{
    public static MusicPlayer Instance;
    [SerializeField] StudioEventEmitter musicEmitter;
    // Start is called before the first frame update
    void Start()
    {
        musicEmitter.Params[0].Value = _threatLevel;
        if (Instance != null) Debug.LogError("Multiple instances of Music Player are in Scene");
        Instance = this;
    }

    private float _threatLevel = 0;
    [SerializeField] float transitionTime = 1;
    public void SetThreatLevel(float value)
    {
        value = Mathf.Clamp01(value);
        if (value == _targetValue) return;
        _targetValue = value;
        StopCoroutine(TransitionThreatLevel());
        StartCoroutine(TransitionThreatLevel());
	}
    float _targetValue = 0;
    IEnumerator TransitionThreatLevel()
    {
        float elapsedTime = 0;
        while(elapsedTime < transitionTime)
        {
            elapsedTime += Time.deltaTime;

            _threatLevel = _targetValue > 0 ? elapsedTime / transitionTime : 1 - (elapsedTime / transitionTime);
            musicEmitter.SetParameter("ThreatLevel", _threatLevel);
            yield return null;
		}
        _threatLevel = _targetValue;
        musicEmitter.SetParameter("ThreatLevel", _threatLevel);
    }

	public void PauseGameMusic(bool pause)
	{
        musicEmitter.EventInstance.setPaused(pause);
        if (!pause && musicEmitter.IsPlaying()) musicEmitter.Play();
	}
}
