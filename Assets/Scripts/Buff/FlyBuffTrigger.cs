using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyBuffTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Destroy(gameObject);
            FindObjectOfType<BuffSystem>().AddFlyBuff();
        }
    }
}