using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollidingFieldOfView : MonoBehaviour
{
    public float radius = 3f;
    [Range(1, 360)] public float angle = 90f;
    public LayerMask targetLayer;
    public LayerMask obstructionLayer;

    public GameObject playerRef;

    public Vector2 aimDirection;

    public bool CanSeePlayer { get; private set; }

    private void Start()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(FovCheck());
    }

    private IEnumerator FovCheck()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            Fov();
        }
    }

    private void Fov()
    {
        Collider2D[] rangeCheck = Physics2D.OverlapCircleAll(transform.position, radius, targetLayer);

        if(rangeCheck.Length > 0 )
        {
            Transform target = rangeCheck[0].transform;
            Vector2 directionToTarget = (target.position - transform.position).normalized;

            if(Vector2.Angle(aimDirection, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector2.Distance(transform.position, target.position);

                if(!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionLayer))
                {
                    CanSeePlayer = true;

                    GameManager.Instance.GameOver();
                }
                else
                {
                    CanSeePlayer = false;
                }
            }
            else if(CanSeePlayer)
            {
                CanSeePlayer = false;
            }
        }
        else if(CanSeePlayer)
        {
            CanSeePlayer = false;
        }
    }

    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.white;
        //UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.forward, radius);
        //
        //Vector3 angle01 = DirectionFromAngle(-transform.eulerAngles.z, -angle / 2);
        //Vector3 angle02 = DirectionFromAngle(-transform.eulerAngles.z, angle / 2);
        //
        //Gizmos.color = Color.yellow;
        //Gizmos.DrawLine(transform.position, transform.position + angle01 * radius);
        //Gizmos.DrawLine(transform.position, transform.position + angle02 * radius);

        if (CanSeePlayer)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, playerRef.transform.position);
        }
    }

    private Vector2 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;

        return new Vector2(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
