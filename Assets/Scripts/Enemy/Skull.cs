using UnityEngine;
public class Skull : MonoBehaviour{
    public float MOVESPEED;
    public float ROTATIONSPEED;
    // public Transform BODY_FOCUS;
    public Transform TARGET;
    public Rigidbody RIGID;

    // Update is called once per frame
    void Start(){
        TARGET = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void FixedUpdate(){
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(TARGET.position - transform.position), ROTATIONSPEED * Time.deltaTime);
        MOVE_TO_TARGET();
    }
    void MOVE_TO_TARGET(){
        RIGID.position += transform.forward * MOVESPEED * Time.deltaTime;
    }
}