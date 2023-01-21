using UnityEngine;
using FMODUnity;

namespace FMODPlus
{
	[System.Serializable]
	public class FMOD_Studio3DEvent
	{
		[Space(10)]
		public EventReference eventReference;
		public FMOD.Studio.EventInstance eventInstance;
		public GameObject gameObjectRef;

		public void Pause(bool pause)
		{
			if (eventInstance.isValid())
			{
				eventInstance.setPaused(pause);
			}
		}

		#region Static Members
		public static bool IsPlaying(FMOD.Studio.EventInstance instance)
		{
			FMOD.Studio.PLAYBACK_STATE state;
			instance.getPlaybackState(out state);
			return state != FMOD.Studio.PLAYBACK_STATE.STOPPED;
		}

		public static void Start(FMOD_Studio3DEvent soundEvent, GameObject gameObjectRef)
		{
			if (gameObjectRef == null)
			{
				Debug.LogError($"SoundEvent: {soundEvent.eventReference.Path} has a NULL gameobject and was not played");
				return;
			}
			soundEvent.gameObjectRef = gameObjectRef;
			soundEvent.eventInstance = RuntimeManager.CreateInstance(soundEvent.eventReference);
			soundEvent.eventInstance.set3DAttributes(RuntimeUtils.To3DAttributes(soundEvent.gameObjectRef));
			soundEvent.eventInstance.start();
		}
		#endregion

	}
}
