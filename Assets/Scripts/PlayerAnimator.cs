using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] Animator _animator;

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
}
