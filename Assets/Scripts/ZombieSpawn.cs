using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawn : MonoBehaviour
{
   [Header("ZombieSpawn var")]
    public GameObject zombiePrefab;
    public Transform zombieSpawnPoint;

    //public GameObject dangerZone;

    private float repeatCycle = 1f;

    private void OntTriggerEnter(Collider other)
    {
        if ( other.gameObject.tag == "Player")
        {
           
        
        
            Debug.Log("Player entered the danger zone");
            InvokeRepeating("EnemySpawner", 1f, repeatCycle);
            Destroy(gameObject,10f);
            gameObject.GetComponent<BoxCollider>().enabled = false; 
        }
    }

    void EnemySpawner()
    {
        Instantiate(zombiePrefab, zombieSpawnPoint.position, zombieSpawnPoint.rotation);
    }

}
