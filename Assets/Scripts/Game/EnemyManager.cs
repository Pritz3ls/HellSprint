// Handles waves of enemies ingame
// will also handle pools to optimize game performance and reduce Garbage
using System.Collections.Generic;
using UnityEngine;
public class EnemyManager : MonoBehaviour{
    public static EnemyManager instance;
    public float WAVESTARTTIME;
    public float WAVETIME;
    public SpawnPool SPAWNPOOL;
    public List<Transform> SPAWNPOINTS = new List<Transform>();
    public int WAVEMODIFIER;
    void Start(){
        if(instance == null){
            instance = this;
        }else{Destroy(gameObject);}

        InvokeRepeating("Spawn",WAVESTARTTIME,WAVETIME);
    }
    // Prototype spawning mechanic, will resort to other creature handling this
    void Spawn(){
        if(GameManager.instance.GAMEOVER){return;}
        for (int i = 0; i < WAVEMODIFIER; i++){    
            GameObject obj = SPAWNPOOL.ReSpawnPrefab();
            if(obj != null){
                obj.transform.position = SPAWNPOINTS[Random.Range(0, SPAWNPOINTS.Count)].position;
                obj.SetActive(true);
            }
        }
    }
}
