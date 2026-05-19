using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarTrigger : MonoBehaviour
{
    public ChunkManager ChunkManager;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Car")
        {
           other.GetComponent<CarCrashAnimation>()?.OnCrash(transform);
           ChunkManager.ReduceSpeedAfterCrush();
        }
    }
}
