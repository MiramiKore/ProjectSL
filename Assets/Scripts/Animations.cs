using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animations : MonoBehaviour
{
    Animator animator;
    static public bool isRun;
    static public bool isSprint;
    static public bool isJump;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Movement();
    }

    public void Movement()
    {
        animator.SetBool("isRunning", isRun);
        animator.SetBool("isSprinting", isSprint);
        animator.SetBool("isJumping", isJump);
    }
}
