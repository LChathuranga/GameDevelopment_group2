using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie1 : MonoBehaviour
{
    [Header("Zombie Health and Damage")]
    public float giveDamage = 5f;

    [Header("Zombie Things")]
    public NavMeshAgent zombieAgent;
    public Transform lookPoint;
    public Camera AttackingRaycastArea;
    public Transform playerBody;
    public LayerMask playerLayer;

    [Header("Zombie Guarding Var")]
    public GameObject[] walkPoints;
    int currentZombiePosition = 0;
    public float zombieSpeed;
    float walkPointRadious = 2;

    [Header("Zombie Attack Var")]
    public float timeBtwAttack;
    bool previouslyAttack;


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
        if (playerInVisionRadious && !playerInAttackingRadious) PursuePlayer();
        if (playerInVisionRadious && playerInAttackingRadious) AttackPlayer();
        

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
        transform.LookAt(walkPoints[currentZombiePosition].transform.position);

    }

    private void PursuePlayer() 
    {
        zombieAgent.SetDestination(playerBody.position);
    }

    private void AttackPlayer()
    {
        zombieAgent.SetDestination(transform.position);
        transform.LookAt(lookPoint);
        if(!previouslyAttack)
        {
            RaycastHit hitInfo;
            if(Physics.Raycast(AttackingRaycastArea.transform.position, AttackingRaycastArea.transform.forward, out hitInfo, attackingRadious))
            {
                Debug.Log("Hit" + hitInfo.transform.name);

                //if we hit whatever we want to hit and that object has player script
                PlayerScript playerBody = hitInfo.transform.GetComponent<PlayerScript>();

                if(playerBody != null)
                {
                    playerBody.PlayerHitDamage(giveDamage);
                }
            }

            previouslyAttack = true;
            Invoke(nameof(ActiveAttacking), timeBtwAttack);

        }
    }

    private void ActiveAttacking()
    {
        previouslyAttack = false;
    }


}
