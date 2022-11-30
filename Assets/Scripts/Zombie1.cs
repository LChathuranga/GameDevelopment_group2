using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie1 : MonoBehaviour
{
    [Header("Zombie Health and Damage")]
    private float zombieHealth = 100f;
    private float presentHealth;
    public float giveDamage = 5f;

     public ProgressBar Pb;

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

    [Header("Zombie Animation")]
    public Animator anim;


    [Header("Zombie mood/status")]
    public float visionRadious;
    public float attackingRadious;
    public bool playerInVisionRadious;
    public bool playerInAttackingRadious;


    private void Awake()
    {
        zombieAgent = GetComponent<NavMeshAgent>();
        presentHealth = zombieHealth;
    }

    private void Update()
    {
        playerInVisionRadious = Physics.CheckSphere(transform.position, visionRadious, playerLayer);
        playerInAttackingRadious = Physics.CheckSphere(transform.position, attackingRadious, playerLayer);

        if (!playerInVisionRadious && !playerInAttackingRadious) Guard();
        if (playerInVisionRadious && !playerInAttackingRadious) PursuePlayer();
        if (playerInVisionRadious && playerInAttackingRadious) AttackPlayer();

        Pb.BarValue = presentHealth;

        //after health is 0 pb is destroyed
        if (presentHealth <= 0)
        {
            Destroy(Pb.gameObject);
            Destroy(gameObject);
        }
        

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
       if (zombieAgent.SetDestination(playerBody.position))
       {
            //animation
            anim.SetBool("Walking",false);
            anim.SetBool("Running",true);
            anim.SetBool("Attacking",false);
            anim.SetBool("Died",false);

       }
       else
       {
            //animation
            anim.SetBool("Walking",false);
            anim.SetBool("Running",false);
            anim.SetBool("Attacking",false);
            anim.SetBool("Died",true);
       }
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

                //animation
            anim.SetBool("Walking",false);
            anim.SetBool("Running",false);
            anim.SetBool("Attacking",true);
            anim.SetBool("Died",false);
            }

            previouslyAttack = true;
            Invoke(nameof(ActiveAttacking), timeBtwAttack);

        }
    }

    private void ActiveAttacking()
    {
        previouslyAttack = false;
    }

    public void zombieHitDamage(float takeDamage)
    {
        presentHealth -= takeDamage;
        if(presentHealth <= 0)
        {
            //animation
            anim.SetBool("Walking",false);
            anim.SetBool("Running",false);
            anim.SetBool("Attacking",false);
            anim.SetBool("Died",true);

            zombieDie();
        }
    }

    private void zombieDie()
    {
        zombieAgent.SetDestination(transform.position);
        zombieSpeed = 0f;
        attackingRadious = 0f;
        visionRadious = 0f;
        playerInAttackingRadious = false;
        playerInVisionRadious = false;
        Object.Destroy(gameObject, 5.0f);
    }


}
