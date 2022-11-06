using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiflePickup : MonoBehaviour
{
    [Header("Rifle's")]
    public GameObject PlayerRifle;
    public GameObject pickupRifle;

    [Header("Rifle Assign Things")]
    public PlayerScript player;
    private float radious = 2.5f;

    private void Awake()
    {
        PlayerRifle.SetActive(false);
    }

    private void Update()
    {
        if(Vector3.Distance(transform.position, player.transform.position) < radious)
        {
            if (Input.GetKeyDown("f"))
            {
                PlayerRifle.SetActive(true);
                pickupRifle.SetActive(false);

                //play pickup sound

                //objective complete
            }
        }
    }

}
