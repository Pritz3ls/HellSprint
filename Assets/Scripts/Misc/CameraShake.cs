using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake instance;
    // Start is called before the first frame update
    void Start()
    {
        if(instance == null){
            instance = this;
        }else{Destroy(gameObject);}
    }
    public void StartShake(float duration, float magnitude){StartCoroutine(Shake(duration, magnitude));}
    IEnumerator Shake(float duration, float magnitude){
        Vector3 originalpos = transform.position;
        float elapsed = 0.0f;
        while(elapsed < duration){
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(x, y, originalpos.z);

            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = Vector3.zero;
    }
}
