using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public static Player_Movement instance;
    public CharacterController CCONTROLLER_COMPONENT;
    public GameObject Weapon_Holder;
    public Transform GROUND_CHECK;
    public LayerMask GROUND_MASK;
    public float GROUND_DISTANCE = 0.4f;
    public float MOVEMENT_SPEED;
    public float GRAVITY = -9.81f;
    public float JUMP = 3f;


    float SPEED;
    float SPEEDFOV = 95;
    Vector3 velocity;
    bool ISGROUNDED;
    // Update is called once per frame
    void Start(){
        SPEED = MOVEMENT_SPEED;
        instance = this;
    }
    void Update()
    {
        ISGROUNDED = Physics.CheckSphere(GROUND_CHECK.position, GROUND_DISTANCE, GROUND_MASK);
        if(ISGROUNDED && velocity.y < 0){
            velocity.y = -2f;
        }else{
            
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        CCONTROLLER_COMPONENT.Move(move * SPEED * Time.deltaTime);

        if(Input.GetButton("Jump")){
            SPEED = Mathf.Lerp(SPEED, MOVEMENT_SPEED * 1.5f, Time.deltaTime);
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, SPEEDFOV, Time.deltaTime);
            if(ISGROUNDED){
                velocity.y = Mathf.Sqrt(JUMP * -2f * GRAVITY);
            }
        }else{
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 75, Time.deltaTime);
            SPEED = MOVEMENT_SPEED;
        }

        velocity.y += GRAVITY * Time.deltaTime;
        CCONTROLLER_COMPONENT.Move(velocity * Time.deltaTime);
    }
    public void Death(){
        CCONTROLLER_COMPONENT.enabled = false;
        GameManager.instance.SETGAMEOVER();
        Weapon_Holder.SetActive(false);
        this.enabled = false;
    }
}
