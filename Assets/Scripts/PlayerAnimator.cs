using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour, IAnimator
{
    [SerializeField] Animator _animator;
    [SerializeField] Transform _model;
	public Animator Animator { get { return _animator; } }
	private void Start()
	{
		var clips = _animator.runtimeAnimatorController.animationClips;
		foreach (var clip in clips) AllAnimationClips.Add(clip.name, clip.length);
	}
	Dictionary<string, float> AllAnimationClips = new Dictionary<string, float>();
	public float RunTime(string animName)
	{
		if (AllAnimationClips.ContainsKey(animName)) return AllAnimationClips[animName];
		return 3f;
	}

	public void SetBool(string paramName, bool value)
    {
        _animator.SetBool(paramName, value);
	}

    public void SetTrigger(string paramName)
    {
        _animator.SetTrigger(paramName);
	}

    public void SetFloat(string paramName, float value)
    {
        _animator.SetFloat(paramName, value);
	}

	public void SetModelAngle(float angle)
	{
		
	}

	public void SetAnimationByName(string animName)
	{
		_animator.Play($"Base Layer.{animName}", 0, 0f);
	}

}
