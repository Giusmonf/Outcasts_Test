using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header("Component References")]
    private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    [SerializeField] private FieldOfView fieldOfView;
    [SerializeField] private CollidingFieldOfView collisionFieldOfView;

    [SerializeField] private Vector2 targetDestination = Vector2.zero;
    [SerializeField] private Vector2 targetDirection = Vector2.zero;
    private Vector2 lastDirection = Vector2.zero;

    [SerializeField] private float movementSpeed;
    [SerializeField] private float minX;
    [SerializeField] private float maxX;
    [SerializeField] private float minY;
    [SerializeField] private float maxY;

    [SerializeField] private float waitForNextDirection;
    [SerializeField] private float currentTimerForNextDirection;

    [SerializeField] private float idleWaitTime;
    [SerializeField] private bool isIdle;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        collisionFieldOfView = GetComponent<CollidingFieldOfView>();
    }

    private void Start()
    {
        ChooseNextDestination();
    }

    private void FixedUpdate()
    {
        MoveEnemy();
        AnimateEnemy();
    }

    private void Update()
    {
        EvaluateWhenToStop();
    }

    private void MoveEnemy()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetDestination, movementSpeed * Time.fixedDeltaTime);

        //rb.MovePosition((targetDirection - rb.position) * movementSpeed * Time.fixedDeltaTime);
    }

    private void EvaluateWhenToStop()
    {
        if(Vector2.Distance(rb.position, targetDestination) <= 0.1f && !isIdle)
        {
            targetDestination = transform.position;

            isIdle = true;

            StartCoroutine(WaitForNextDestination());
        }
    }

    private IEnumerator WaitForNextDestination()
    {
        yield return new WaitForSeconds(idleWaitTime);

        ChooseNextDestination();
    }

    private void ChooseNextDestination()
    {
        targetDestination = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));

        targetDirection = (targetDestination - rb.position).normalized;
        lastDirection = targetDirection;

        isIdle = false;

        fieldOfView.SetAimDirection(RotateVector2(targetDirection, 90));
        fieldOfView.SetOrigin(Vector2.zero);
        fieldOfView.SetEnemyPosition(transform.localPosition);
        collisionFieldOfView.aimDirection = targetDirection;
    }

    private Vector2 RotateVector2(Vector2 vec, float angle)
    {
        float newAngle = Mathf.Atan2(vec.y, vec.x) + angle * Mathf.Deg2Rad;
        return new Vector2(Mathf.Cos(newAngle), Mathf.Sin(newAngle));
    }

    private void AnimateEnemy()
    {
        animator.SetFloat("MovementX", targetDirection.x);
        animator.SetFloat("MovementY", targetDirection.y);

        animator.SetFloat("Speed", (targetDirection * movementSpeed).sqrMagnitude);
    }
}
