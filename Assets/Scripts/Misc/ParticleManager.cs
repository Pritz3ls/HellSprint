using UnityEngine;
public class ParticleManager : MonoBehaviour{
    public static ParticleManager instance;
    public SpawnPool SPAWNPOOL;
    void Start(){
        if(instance == null){
            instance = this;
        }else{Destroy(gameObject);}
    }
    public void Spawn(Vector3 objectTRANSFORM){
        GameObject obj = SPAWNPOOL.ReSpawnPrefab();
        if(obj != null){
            obj.transform.position = objectTRANSFORM;
            obj.SetActive(true);
            obj.GetComponent<ParticleSystem>().Play();
        }
    }
}