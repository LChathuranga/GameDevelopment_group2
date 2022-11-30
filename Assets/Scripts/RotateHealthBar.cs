using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateHealthBar : MonoBehaviour
{
   public Transform MainCamera;

    private void LateUpdate()
    {
         transform.LookAt(transform.position + MainCamera.forward);
         //rotate 180 degrees on y axis
            transform.Rotate(0, 180, 0);
    }
}
