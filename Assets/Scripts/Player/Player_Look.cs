using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Look : MonoBehaviour
{
    // PLAYER LOOK ATTRIBUTES
    public float MOUSE_SENSITIVITY;
    public Transform PLAYER_BODY;
    private float xROTATION = 0f;
    public Weapon weapon;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * MOUSE_SENSITIVITY * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * MOUSE_SENSITIVITY * Time.deltaTime;

        xROTATION -= mouseY;
        xROTATION = Mathf.Clamp(xROTATION, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xROTATION, 0f, 0f);
        PLAYER_BODY.Rotate(Vector3.up * mouseX);

        if(Input.GetMouseButton(0)){
            weapon.SHOOT();
        }
    }
}
