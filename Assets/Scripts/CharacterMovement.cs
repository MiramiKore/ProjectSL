using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{
    [Header("Move")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;
    private Vector2 _moveDirection;

    [Header("Sprint")]
    [SerializeField] private float sprintSpeed;
    private float moveSpeedConst;

    [Header("Jump")]
    [SerializeField] private float jumpForce;

    Rigidbody rb;

    public void Start()
    {
        rb = GetComponent<Rigidbody>();
        moveSpeedConst = moveSpeed;
    }

    private void Update()
    {
        Move(_moveDirection);
    }

    #region Movement Script
    public void OnMove(InputAction.CallbackContext context)
    {
        _moveDirection = context.ReadValue<Vector2>();
    }

    public void Move(Vector2 direction)
    {
        float scaledMoveSpeed = moveSpeed * Time.deltaTime;

        Vector3 moveDirection = new Vector3(direction.x, 0, direction.y);
        transform.position += moveDirection * scaledMoveSpeed;

        //Поворот персонажа
        if (moveDirection != Vector3.zero)
        {
            Animations.isRun = true;
            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
        else
        {
            Animations.isRun = false;
        }    
    }
    #endregion

    #region Sprint Script
    public void onSprint(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            moveSpeed = sprintSpeed;
            Animations.isSprint = true;
        }
        else if (context.canceled)
        {
            moveSpeed = moveSpeedConst;
            Animations.isSprint = false;
        }
    }
    #endregion

    #region Jump Script
    public void onJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            rb.AddForce(new Vector3(0, jumpForce, 0));
            Animations.isJump = true;
        }
        else
        {
            Animations.isJump = false;
        }
    }
    #endregion
}
