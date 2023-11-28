using System.Collections;
using UnityEngine;
using UnityEngine.TextCore;

public class Enemy : MonoBehaviour
{
    public int MAXHEALTH = 3;
    int HEALTH;
    public int SCORE = 1;
    public Color HURT_COLOR;
    Material OBJ_MAT;
    void Start(){
        Renderer renderer = GetComponentInChildren<Renderer>();
        OBJ_MAT = renderer.material;
        HEALTH = MAXHEALTH;
    }
    public void DAMAGE(int damage){
        HEALTH -= damage;
        if(HEALTH > 0){
            if(gameObject.activeSelf){
                StopCoroutine(FlashCoroutine());
                StartCoroutine(FlashCoroutine());
            }
        }else{DEATH();}
    }
    void OnCollisionEnter(Collision col){
        if(col.gameObject.tag == "Player"){
            Player_Movement.instance.Death();
        }
    }
    void DEATH(){
        HEALTH = MAXHEALTH;
        OBJ_MAT.SetColor("_Color", Color.white);
        ParticleManager.instance.Spawn(transform.position);
        SoundManager.instance.PLAY_ENEMY_DEATH();
        gameObject.SetActive(false);
    }
    IEnumerator FlashCoroutine(){
        float duration = 0.2f;
        while(duration > 0){
            duration -= Time.deltaTime;
            OBJ_MAT.SetColor("_Color", HURT_COLOR);
            yield return null;
        }
        OBJ_MAT.SetColor("_Color", Color.white);
    }
}
