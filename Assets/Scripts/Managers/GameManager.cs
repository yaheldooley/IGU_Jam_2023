using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SceneLoader))]
public class GameManager : MonoBehaviour
{
	public static GameManager Instance;
	SceneLoader _sceneLoader;
	public PlayerController player;
	PlayerInputs _actions;

	private void Awake()
	{
		if(Instance != null)
		{
			Destroy(this.gameObject);
			return;
		}
		Instance = this;
		DontDestroyOnLoad(this.gameObject);

		_actions = new PlayerInputs();

		_sceneLoader = GetComponent<SceneLoader>();
		_state = GameState.Menu;
		ChangeGameState(GameState.Game);
	}
	private void Start()
	{
		_actions.Enable();
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

	private void Update()
	{
		EnemyManager.Update();

	}

	private GameState _state = GameState.Game;
	public void ChangeGameState(GameState newState)
	{
		if (newState == _state) return;

		switch(newState)
		{
			case GameState.Menu:
				if (MusicPlayer.Instance) MusicPlayer.Instance.PauseGameMusic(true);
				// Set Menu Controls 
				// Freeze anything that could be moving
				break;

			case GameState.CutScene:
				// Set Cutscene controls - Advance to next or skip button
				break;

			case GameState.LoadStart:
				// Kill all controls
				// fade to black
				break;

			case GameState.Loading:
				// show loading screen with progress bar
				break;

			case GameState.LoadFinished:
				// Fade from black

				break;

			case GameState.Game:
				if (MusicPlayer.Instance) MusicPlayer.Instance.PauseGameMusic(false);
				_actions.UI.Disable();
				_actions.Gameplay.Enable();
				// Set Game controls
				// Unfreeze anything that should be moving
				break;

			case GameState.Death:
				if (player)
				{
					player.Dead();
					_actions.Gameplay.Disable();
					_actions.UI.Enable();
					if (CanvasManager.Instance) CanvasManager.Instance.ShowDeathScreen();
				}
				break;
		}
	}

	public void ReloadSceneFromLast()
	{
	
	}

	public void QuitApp()
	{
		Application.Quit();
	}

	public enum GameState
	{
		Menu,
		CutScene,
		LoadStart,
		Loading,
		LoadFinished,
		Game,
		Death,
	}
}
