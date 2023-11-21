using System.Collections;
using UnityEngine;
public class Weapon : MonoBehaviour{
    public int WEAPON_DAMAGE;
    public float FIRERATE;
    public Camera MAINCAMERA;
    public Transform WEAPONPOINT;
    public LayerMask RAYCASTHITONLY;
    public TrailRenderer NAILTRAIL;
    public ParticleSystem SHOOT_EFFECTS;
    public Animator animator;

    private float NEXT_TIME_FIRE = 0f;

    public void SHOOT(){
        if(Time.time >= NEXT_TIME_FIRE){
            NEXT_TIME_FIRE = Time.time + 1f/FIRERATE;
            SHOOT_EFFECTS.Play();
            animator.Play("weapon_shoot_anim");

            RaycastHit hit;
            if(Physics.Raycast(MAINCAMERA.transform.position, MAINCAMERA.transform.forward, out hit, 100, RAYCASTHITONLY)){
                TrailRenderer trail = Instantiate(NAILTRAIL, WEAPONPOINT.position, Quaternion.identity);
                StartCoroutine(HITOBJECT(trail, hit));
            }
        }
    }
    IEnumerator HITOBJECT(TrailRenderer trail, RaycastHit hit){
        float time = 0f;
        Vector3 startPosition = trail.transform.position;
        while(time < 1){
            trail.transform.position = Vector3.Lerp(startPosition, hit.point, time);
            time += Time.deltaTime / trail.time;
            yield return null;
        }
        trail.transform.position = hit.point;
        Enemy enemy_hit = hit.transform.GetComponent<Enemy>();
        if(enemy_hit != null){
            enemy_hit.DAMAGE(WEAPON_DAMAGE);
        }
        Destroy(trail.gameObject, trail.time);
    }
}