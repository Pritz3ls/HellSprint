// Will handel optimization for Garbage
// and a ton of instances of prefabs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPool : MonoBehaviour
{
    public GameObject TOSPAWNPREFAB;
    public int AMOUNTTOSPAWN;
    public Transform PARENT;
    private List<GameObject> pooledPrefabs = new List<GameObject>();
    void Awake(){
        SpawnPrefab();
    }
    private void SpawnPrefab(){
        for (int i = 0; i < AMOUNTTOSPAWN; i++){
            GameObject obj = Instantiate(TOSPAWNPREFAB, PARENT);
            obj.SetActive(false);
            pooledPrefabs.Add(obj);
        }
    }
    public GameObject ReSpawnPrefab(){
        for (int i = 0; i < pooledPrefabs.Count; i++){
            if(!pooledPrefabs[i].activeInHierarchy){
                return pooledPrefabs[i];
            }
        }
        return null;
    }
}
