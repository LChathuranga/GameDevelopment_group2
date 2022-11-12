using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie2 : MonoBehaviour
{
   
    [Header("Zombie Health and Damage")]
    private float zombieHealth = 100f;
    private float presentHealth;
    public float giveDamage = 5f;

    [Header("Zombie Things")]
    public NavMeshAgent zombieAgent;
    public Transform lookPoint;
    public Camera AttackingRaycastArea;
    public Transform playerBody;
    public LayerMask playerLayer;

    [Header("Zombie Standing Var")]
    
    public float zombieSpeed;


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

        if (!playerInVisionRadious && !playerInAttackingRadious) Idle();
        if (playerInVisionRadious && !playerInAttackingRadious) PursuePlayer();
        if (playerInVisionRadious && playerInAttackingRadious) AttackPlayer();
        

    }

    private void Idle()
    {
        zombieAgent.SetDestination(transform.position);

    }

    private void PursuePlayer() 
    {
       if (zombieAgent.SetDestination(playerBody.position))
       {
            //animation
            // anim.SetBool("Walking",false);
            // anim.SetBool("Running",true);
            // anim.SetBool("Attacking",false);
            // anim.SetBool("Died",false);

       }
       else
       {
            //animation
            // anim.SetBool("Walking",false);
            // anim.SetBool("Running",false);
            // anim.SetBool("Attacking",false);
            // anim.SetBool("Died",true);
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
            // anim.SetBool("Walking",false);
            // anim.SetBool("Running",false);
            // anim.SetBool("Attacking",true);
            // anim.SetBool("Died",false);
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
            // anim.SetBool("Walking",false);
            // anim.SetBool("Running",false);
            // anim.SetBool("Attacking",false);
            // anim.SetBool("Died",true);

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
