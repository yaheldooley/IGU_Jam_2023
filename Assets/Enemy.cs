using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	NavMeshMover _mover;

	FieldOfView _view;
    Transform targetTransform;
	bool _targetInRange = false;
	List<GameObject> targetsWithinHostileRange = new List<GameObject>();

	[SerializeField] List<Attack> _attacks;
	[SerializeField] AttackRange _attackRange;

	Attacker _attacker;

	private void Awake()
	{
		_mover = GetComponent<NavMeshMover>();
		_view = GetComponent<FieldOfView>();
		_attacker = GetComponent<Attacker>();
	}

	private void OnEnable()
	{
		_state = EnemyState.Idle;
	}

	

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == LayerMask.NameToLayer("Target") && !targetsWithinHostileRange.Contains(other.gameObject))
		{
			//Debug.Log("Has target");
			if(targetsWithinHostileRange.Count < 1) _view.SearchForTargets();
			targetsWithinHostileRange.Add(other.gameObject);
			_targetInRange = true;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.layer == LayerMask.NameToLayer("Target") && targetsWithinHostileRange.Contains(other.gameObject))
		{
			targetsWithinHostileRange.Remove(other.gameObject);
			_targetInRange = targetsWithinHostileRange.Count > 0;
			if (targetTransform != null && other.gameObject == targetTransform.gameObject)
			{
				targetTransform = null;
				_state = EnemyState.Idle;
			}
			if (targetsWithinHostileRange.Count < 1) _view.StopSearching();
		}
	}

	private EnemyState _state;
	private void Update()
	{
		switch(_state)
		{
			case EnemyState.Disabled:
				return;

			case EnemyState.Idle:
				if (_targetInRange)
				{
					if (EngagingATargetWithinRange())
					{
						return;
					}
					if(_view.visibleTargets.Count > 0 && targetTransform == null)
					{
						targetTransform = Helpers.GetNearestTransform(_view.visibleTargets, transform.position);
						_state = EnemyState.Hostile;
					}
				}
				else
				{
					// eat a bumper
				}
				break;

			case EnemyState.Hostile:
				if (EngagingATargetWithinRange())
				{
					return;
				}
				if (targetTransform != null)
				{
					_mover.SetDestination(targetTransform.position);
				}
				else
				{
					_state = EnemyState.Idle; 
					return;
				}
				break;

			case EnemyState.Attack:
				//CanvasManager.Instance.text.text = $"Attacking is: {_attacker.Attacking}";
				if (!_attacker.Attacking)
				{
					_mover.EnableMovement();
					_state = EnemyState.Hostile;
				}
				break;
		}
	}
	private bool EngagingATargetWithinRange()
	{
		if (_state == EnemyState.Disabled) return false;

		if (_state != EnemyState.Attack && _attackRange.AllInRange.Count > 0 && !_attacker.Attacking)
		{
			_state = EnemyState.Attack;
			_mover.DisableMovement();
			_attacker.Attack(Random.Range(0,5), transform.position);
			return true;
		}
		return false;
	}


	public enum EnemyState
	{
		Disabled,
		Idle,
		Hostile,
		Attack,
	}

	// Idle
	// Has sight to player chase
	// If within attack range, stop mover, attack animation
	
}
