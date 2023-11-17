using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public CharacterController CCONTROLLER_COMPONENT;
    public float MOVEMENT_SPEED;
    public float GRAVITY = -9.81f;

    public Transform GROUND_CHECK;
    public float GROUND_DISTANCE = 0.4f;
    public LayerMask GROUND_MASK;

    Vector3 velocity;
    bool ISGROUNDED;
    // Update is called once per frame
    void Update()
    {
        ISGROUNDED = Physics.CheckSphere(GROUND_CHECK.position, GROUND_DISTANCE, GROUND_MASK);
        if(ISGROUNDED && velocity.y < 0){
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        CCONTROLLER_COMPONENT.Move(move * MOVEMENT_SPEED * Time.deltaTime);

        velocity.y += GRAVITY * Time.deltaTime;
        CCONTROLLER_COMPONENT.Move(velocity * Time.deltaTime);
    }
}
