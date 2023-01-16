using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private float _loadProgress = 0;
    public float LoadProgress { get => _loadProgress; }

    private bool _isLoading = false;

    [SerializeField] UnityEvent OnSceneLoadStartEvent;
    [SerializeField] UnityEvent OnSceneLoadFinishedEvent;

    public void LoadScene(int sceneIndex)
    {
        if (_isLoading) return;
        StartCoroutine(LoadSceneAsync(sceneIndex));
	}
    public void LoadScene(string sceneName)
    {
        if (_isLoading) return;
        try
        {
            int index = SceneManager.GetSceneByName(sceneName).buildIndex;
            LoadScene(index);
        }
        catch{
            Debug.LogError($"No scene named {sceneName} found");
		}
        
    }

    IEnumerator LoadSceneAsync(int sceneIndex)
    {
        OnSceneLoadStartEvent?.Invoke(); // CanvasManager might cover screne with image

        _isLoading = true;
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        _loadProgress = 0;
        while(!operation.isDone)
        {
            _loadProgress = Mathf.Clamp01(operation.progress / .9f);
            yield return null;
		}
        _isLoading = false;
        _loadProgress = 0;
        OnSceneLoadFinishedEvent?.Invoke();
    }
}
