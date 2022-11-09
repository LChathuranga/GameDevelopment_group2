using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;   

public class Zombie1 : MonoBehaviour
{
<<<<<<< HEAD
    [Header("Zombie Health and Damage")]
    public float giveDamage = 5f;


=======
>>>>>>> d1b2468588c3c81883d1e2e510bfdaa7cc61c6f0
    [Header("Zombie Things")]
    public NavMeshAgent zombieAgent;
    public Transform playerBody;
    public Transform LookPoint;
<<<<<<< HEAD
    public Camera AttackingRacastingArea;
=======
>>>>>>> d1b2468588c3c81883d1e2e510bfdaa7cc61c6f0
    public LayerMask PlayerLayer;

    [Header("Zombie guarding Var")]
    public GameObject[] walkPoints;
    int currentZombiePosition = 0;
    public float zombieSpeed;
    float walkingPointRadius = 2;

<<<<<<< HEAD
    [Header("Zombie attacking Var")]
    public float timeBtwAttack;
    bool previouslyAttack;

=======
>>>>>>> d1b2468588c3c81883d1e2e510bfdaa7cc61c6f0
    [Header("Zombie mood/state")]
    public float visionRadius;
    public float attackingRadius;
    public bool playerInVisionRadius;
    public bool playerInAttackingRadius;

    private void Awake()
    {
        zombieAgent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        playerInVisionRadius = Physics.CheckSphere(transform.position, visionRadius, PlayerLayer);
        playerInAttackingRadius = Physics.CheckSphere(transform.position, attackingRadius, PlayerLayer);

        if(!playerInVisionRadius && !playerInAttackingRadius) Guard();
        if(playerInVisionRadius && !playerInAttackingRadius) Pursueplayer();
<<<<<<< HEAD
        if(playerInVisionRadius && playerInAttackingRadius) AttackPlayer();
=======
>>>>>>> d1b2468588c3c81883d1e2e510bfdaa7cc61c6f0
    }
    private void Guard()
    {
        if(Vector3.Distance(walkPoints[currentZombiePosition].transform.position, transform.position) < walkingPointRadius)
        {
            currentZombiePosition = Random.Range(0, walkPoints.Length);
            if(currentZombiePosition >= walkPoints.Length)
            {
                currentZombiePosition = 0;
            }
        }
        transform.position = Vector3.MoveTowards(transform.position, walkPoints[currentZombiePosition].transform.position, Time.deltaTime * zombieSpeed);
        //change zombie facing
        transform.LookAt(walkPoints[currentZombiePosition].transform.position);
    }
    private void Pursueplayer()
    {
        zombieAgent.SetDestination(playerBody.position);
    }
<<<<<<< HEAD
    private void AttackPlayer()
    {
        zombieAgent.SetDestination(transform.position);
        transform.LookAt(LookPoint);
        if(!previouslyAttack)
        {
            RaycastHit hitInfo;
            if(Physics.Raycast(AttackingRacastingArea.transform.position, AttackingRacastingArea.transform.forward, out hitInfo, attackingRadius))
            {
                Debug.Log("Attacking" + hitInfo.transform.name);

                PlayerScript playerBody = hitInfo.transform.GetComponent<PlayerScript>();

                if(playerBody != null)
                {
                    playerBody.playerHitDamage(giveDamage);
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
=======
>>>>>>> d1b2468588c3c81883d1e2e510bfdaa7cc61c6f0
}
