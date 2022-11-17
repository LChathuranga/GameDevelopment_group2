using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
  //spawn object
    public GameObject spawnObject;
    //spawn position
    public Transform spawnPosition;
    //spawn repeat rate
    public float spawnRate = 1f;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered");

        //if player enters trigger
        if (other.gameObject.tag == "Player")
        {
            InvokeRepeating("newEnemySpawn", 0f, spawnRate);

            Destroy(gameObject,10f);

            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }

    private void newEnemySpawn()
    {
        //spawn object
        Instantiate(spawnObject, spawnPosition.position, spawnPosition.rotation);
    }
}
