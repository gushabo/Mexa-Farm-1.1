using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class CharacterController2D : MonoBehaviour
{
    new Rigidbody2D rigidbody2D;
    [SerializeField] float speed = 2f;
    Vector2 motionVector;
    public Vector2 LastMotionVector;
    Animator animator;
    public bool Moving;

    void Awake()
    {
        //Get the script the rigidbody of the gameObject and the animator
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    public void Update()
    {
        //Get the position values 
        float Vertical = Input.GetAxisRaw("Vertical");
        float Horizontal = Input.GetAxisRaw("Horizontal");

        //Set the values in the animator
        motionVector = new Vector2(Horizontal, Vertical);
        animator.SetFloat("Horizontal", Horizontal);
        animator.SetFloat("Vertical", Vertical);

        //Detect if the player is moving or not
        Moving = Horizontal != 0 || Vertical != 0;
        animator.SetBool("Moving", Moving);

        if (Horizontal != 0 || Vertical != 0)
        {
            LastMotionVector = new Vector2(Horizontal, Vertical).normalized;
            
            animator.SetFloat("LastHorizontal", Horizontal);
            animator.SetFloat("LastVertical", Vertical);
        }

    }

    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        //Add to the rigidbody in the velocity field the position and the values and multiply by 2
        rigidbody2D.MovePosition(rigidbody2D.position + motionVector * speed * Time.fixedDeltaTime);

    }
}
