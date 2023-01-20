using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SceneLoader))]
public class GameManager : MonoBehaviour
{
	public static GameManager Instance;
	SceneLoader _sceneLoader;
	
	private void Awake()
	{
		if(Instance != null)
		{
			Destroy(this.gameObject);
			return;
		}
		_sceneLoader = GetComponent<SceneLoader>();
	}

	public void LoadScene(int index)
	{
		_sceneLoader.LoadScene(index);
		
	}

	public void SceneLoaded()
	{
		// Hide black panel
		// Hide progress bar
		// set players position etc
	}


}
