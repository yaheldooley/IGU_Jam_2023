using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAnimator : MonoBehaviour, IAnimator
{
    [SerializeField] Animator _animator;
    public Animator Animator { get { return _animator; } }
    [SerializeField] Transform _model;
    [SerializeField] int Type;

    Dictionary<string, float> AllAnimationClips = new Dictionary<string, float>();
    public float RunTime(string animName)
    {
        if (AllAnimationClips.ContainsKey(animName)) return AllAnimationClips[animName];
        return 3f;
	}

    
    private void Start()
	{
        _animator.SetFloat("type", Type);
        var clips = _animator.runtimeAnimatorController.animationClips;
        foreach (var clip in clips) AllAnimationClips.Add(clip.name, clip.length);
    }
    public void SetModelAngle(float angle)
    {
        _model.localEulerAngles = new Vector3(0, angle, 0);
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

    public void SetAnimationByName(string animName)
    {
        _animator.Play($"Base Layer.{animName}", 0, 0f);
    }
}

