using UnityEngine;

public interface IAnimator
{
    public void SetModelAngle(float angle);
    public void SetBool(string paramName, bool value);
    public void SetTrigger(string paramName);
    public void SetFloat(string paramName, float value);
    public float RunTime(string animName);
    public static float ActiveAnimRunTime(Animator anim)
    {
        if (anim.GetCurrentAnimatorClipInfo(0).Length == 0) return 0;
        return anim.GetCurrentAnimatorClipInfo(0)[0].clip.length;
    }

    public Animator Animator { get; }
    public void SetAnimationByName(string animName);

}

