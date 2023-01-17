using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public static class CameraTracker
{
    private static CinemachineBrain _brain;
    public static void BrainReference(CinemachineBrain brain)
    {
        _brain = brain;
	}
    public static Transform ActiveCameraTransform {
        get
        {
            if (_brain != null && _brain.ActiveVirtualCamera != null) return _brain.ActiveVirtualCamera.VirtualCameraGameObject.transform;
            return null;
		}
	}
    
}
