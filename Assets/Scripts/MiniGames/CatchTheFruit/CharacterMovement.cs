using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{

    [SerializeField] float speed = 2f;
    [SerializeField] public float points = 0f;
    Rigidbody2D rgbd;
    Vector2 motionVector;

    void Start()
    {
        rgbd = GetComponent<Rigidbody2D>();    
    }
    
    void Update()
    {
        float Horizontal = Input.GetAxisRaw("Horizontal");
        motionVector = new Vector2(Horizontal,0);
    }

    private void FixedUpdate() {
        Move();
    }

    private void Move()
    {
        rgbd.MovePosition(rgbd.position + motionVector * speed * Time.fixedDeltaTime);

    }
}
