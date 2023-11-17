using UnityEngine;

public class CameraTilt : MonoBehaviour{
    public float LEAN_ANGLE = 5f;
    float curAngle;
    float targetAngle;
    // float maxRot = -5.0f;
    float rate = 2.0f;
    void Update(){
        LeanCamera(Input.GetAxis("Horizontal"));
    }
    public void LeanCamera(float axis)
    {
        curAngle = transform.transform.localEulerAngles.z;
        targetAngle = LEAN_ANGLE - axis;

        if (axis == 0.0f)targetAngle = 0.0f;
        transform.transform.localRotation = Quaternion.Lerp(transform.transform.localRotation,Quaternion.Euler(transform.transform.localRotation.x, transform.transform.localRotation.y, axis * LEAN_ANGLE), Time.deltaTime * rate);
    }
}