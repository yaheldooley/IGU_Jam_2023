using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public Transform eyeTransform;
    public float viewRadius;
    [Range(0,360)]
    public float viewAngle;

    public LayerMask targetMask;
    [SerializeField] LayerMask obstacleMask;

    public List<Transform> visibleTargets = new List<Transform>();
    [SerializeField] float delayBetweenTargetChecks = 1;
    public void SearchForTargets()
    {
        StopAllCoroutines();
        StartCoroutine(FindTargetsWithDelay());
	}

    public void StopSearching()
    {
        StopAllCoroutines();
        visibleTargets.Clear();
	}

    IEnumerator FindTargetsWithDelay()
    {
        while(true)
        {
            FindVisibleTargets();
            yield return new WaitForSeconds(delayBetweenTargetChecks);
		}
	}

    void FindVisibleTargets()
    {
        visibleTargets.Clear();
        Collider[] targetsInViewRadius = Physics.OverlapSphere(eyeTransform.position, viewRadius, targetMask);
        //Debug.Log($"Has {targetsInViewRadius.Length} targets");

        for (int i = 0; i < targetsInViewRadius.Length; i++)
		{
            
            Transform target = targetsInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - eyeTransform.position).normalized;
            if (Vector3.Angle(eyeTransform.forward, dirToTarget) < viewAngle / 2)
            {
                float distToTarget = Vector3.Distance(eyeTransform.position, target.position);
                if(!Physics.Raycast (eyeTransform.position, dirToTarget, distToTarget, obstacleMask))
                {
                    visibleTargets.Add(target);
				}
			}
		}
	}

    public Vector3 DirectionFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal) angleInDegrees += transform.eulerAngles.y;
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
	}

    
}
