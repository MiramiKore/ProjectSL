using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{
    public float moveSpeed = 5;
    private Vector2 _moveDirection;
    public bool isRun;

    Rigidbody rb;
    Animator animator;

    public void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Move(_moveDirection);
        Rotation();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _moveDirection = context.ReadValue<Vector2>();
    }

    public void Move(Vector2 direction)
    {
        float scaledMoveSpeed = moveSpeed * Time.deltaTime;

        Vector3 moveDirection = new Vector3(direction.x, 0, direction.y);
        transform.position += moveDirection * scaledMoveSpeed;

        animator.SetBool("isRunning", isRun);

        if (direction.x != 0 || direction.y !=0)
        {
            isRun = true;
        }
        else
        {
            isRun = false;
        }    
    }

    private void Rotation()
    {
        Vector3 currentPosition = transform.position;

        Vector3 newPosition = new Vector3(_moveDirection.x, 0, _moveDirection.y);

        Vector3 positionToLookAt = currentPosition + newPosition;

        transform.LookAt(positionToLookAt);
    }
}
