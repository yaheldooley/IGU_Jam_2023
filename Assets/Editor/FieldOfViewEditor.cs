using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FieldOfView))]
public class FieldOfViewEditor : Editor
{
	private void OnSceneGUI()
	{
		FieldOfView fow = (FieldOfView)target;
		Handles.color = Color.white;
		Handles.DrawWireArc(fow.eyeTransform.position, Vector3.up, Vector3.forward, 360, fow.viewRadius);
		Vector3 viewAngleA = fow.DirectionFromAngle(-fow.viewAngle / 2, false);
		Vector3 viewAngleB = fow.DirectionFromAngle(fow.viewAngle / 2, false);

		Handles.DrawLine(fow.eyeTransform.position, fow.eyeTransform.position + viewAngleA * fow.viewRadius);
		Handles.DrawLine(fow.eyeTransform.position, fow.eyeTransform.position + viewAngleB * fow.viewRadius);
	}
}
