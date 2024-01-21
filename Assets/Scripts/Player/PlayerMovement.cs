using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 moveInput;
    private Rigidbody2D rb;

    [SerializeField] private Animator animator;
    [SerializeField] private float moveSpeed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        MovePlayer();
        AnimatePlayer();
    }

    private void MovePlayer()
    {
        rb.MovePosition(rb.position + moveInput * moveSpeed * Time.fixedDeltaTime);
    }

    private void AnimatePlayer()
    {
        animator.SetFloat("MovementX", moveInput.x);
        animator.SetFloat("MovementY", moveInput.y);

        animator.SetFloat("Speed", (moveInput * moveSpeed).sqrMagnitude);
    }

    private void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }
}
