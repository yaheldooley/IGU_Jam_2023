using FMODPlus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
public class TestSoundEventClass : MonoBehaviour
{
    [SerializeField] StudioEventEmitter soundEvent;

    bool paused = false;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            paused = !paused;
            soundEvent.EventInstance.setPaused(paused);
        }
    }
}
