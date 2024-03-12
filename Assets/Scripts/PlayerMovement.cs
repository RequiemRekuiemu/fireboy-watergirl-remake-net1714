using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float playerSpeed = 40f;

    public CharacterController2D controller;

    public Animator animator;

    private float horizontalMove = 0f;

    private bool jump = false;

    private bool crouch = false;

    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (string.Equals(player.name, "Player1", StringComparison.OrdinalIgnoreCase))
        {
            horizontalMove = Input.GetAxisRaw("Player1_Horizontal") * playerSpeed;
            if (Input.GetButtonDown("Player1_Jump"))
            {
                jump = true;
                animator.SetBool("IsJumping", true);
            }
            if (Input.GetButtonDown("Player1_Crouch"))
            {
                crouch = true;
            }
            else if (Input.GetButtonUp("Player1_Crouch"))
            {
                crouch = false;
            }
        }
        if (string.Equals(player.name, "Player2", StringComparison.OrdinalIgnoreCase))
        {
            horizontalMove = Input.GetAxisRaw("Player2_Horizontal") * playerSpeed;
            if (Input.GetButtonDown("Player2_Jump"))
            {
                jump = true;
                animator.SetBool("IsJumping", true);
            }
            if (Input.GetButtonDown("Player2_Crouch"))
            {
                crouch = true;
            }
            else if (Input.GetButtonUp("Player2_Crouch"))
            {
                crouch = false;
            }
        }
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
    }

    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
    }

    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }
}
