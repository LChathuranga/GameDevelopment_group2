using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie1 : MonoBehaviour
{
    [Header("Zombie Things")]
    public NavMeshAgent zombieAgent;
    public Transform lookPoint;
    public LayerMask playerLayer;

    [Header("Zombie Guarding Var")]
    public GameObject[] walkPoints;
    int currentZombiePosition = 0;
    public float zombieSpeed;
    float walkPointRadious = 2;

    [Header("Zombie mood/status")]
    public float visionRadious;
    public float attackingRadious;
    public bool playerInVisionRadious;
    public bool playerInAttackingRadious;


    private void Awake()
    {
        zombieAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        playerInVisionRadious = Physics.CheckSphere(transform.position, visionRadious, playerLayer);
        playerInAttackingRadious = Physics.CheckSphere(transform.position, attackingRadious, playerLayer);

        if (!playerInVisionRadious && !playerInAttackingRadious) Guard();

    }

    private void Guard()
    {
        if (Vector3.Distance(walkPoints[currentZombiePosition].transform.position, transform.position) < walkPointRadious)
        {
            currentZombiePosition = Random.Range(0, walkPoints.Length);
            if(currentZombiePosition >= walkPoints.Length)
            {
                currentZombiePosition = 0;
            }
        }
        transform.position = Vector3.MoveTowards(transform.position, walkPoints[currentZombiePosition].transform.position, Time.deltaTime * zombieSpeed);
        // change zombie facing
    }
}
