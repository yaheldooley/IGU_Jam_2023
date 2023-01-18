using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacker : MonoBehaviour
{
    IAnimator _anim;
	public bool Attacking { get { return _attacking; } }
	private void Awake()
	{
		_anim = GetComponent<IAnimator>();
	}

	bool _attacking = false;
	public void Attack(int attackIndex, Vector3 targetPos)
    {
		if (_attacking) return;
		_attacking = true;
		StartCoroutine(AttackSequence(attackIndex, targetPos));
	}

	IEnumerator AttackSequence(int attackIndex, Vector3 targetPos)
	{
		targetPos.y = transform.position.y;
		bool rotating = true;
		while(rotating)
		{
			// Determine which direction to rotate towards
			Vector3 targetDirection = targetPos - transform.position;

			// The step size is equal to speed times frame time.
			float singleStep = 5 * Time.deltaTime;

			// Rotate the forward vector towards the target direction by one step
			Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);

			// Draw a ray pointing at our target in
			Debug.DrawRay(transform.position, newDirection, Color.red);

			// Calculate a rotation a step closer to the target and applies rotation to this object
			transform.rotation = Quaternion.LookRotation(newDirection);
			float angle = Vector3.SignedAngle(newDirection, transform.forward, Vector3.up);
			if (angle > -2 && angle < 2) rotating = false;
			yield return null;
		}
		_anim.SetFloat("attackType", attackIndex);
		_anim.SetTrigger("attack");
		float waitTime = IAnimator.ActiveAnimRunTime(_anim.Animator);
		CanvasManager.Instance.text.text = waitTime.ToString();
		yield return new WaitForSeconds(waitTime + 0.5f);
		_attacking = false;
	}
}
