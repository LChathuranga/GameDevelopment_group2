using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    //spawn object
    public GameObject spawnObject;
    //spawn position
    public Transform spawnPosition;
    //spawn repeat rate
    public float spawnRate = 1f;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered");

        //if player enters trigger
        if (other.gameObject.tag == "Player")
        {
            InvokeRepeating("EnemySpawn1", 0f, spawnRate);

            Destroy(gameObject,11f);

            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }

    void EnemySpawn1()
    {
        //spawn object
        Instantiate(spawnObject, spawnPosition.position, spawnPosition.rotation);
    }
    


}
