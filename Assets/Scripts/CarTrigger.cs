using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarTrigger : MonoBehaviour
{
    public ChunkManager ChunkManager;
    public Car Car;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Car")
        {
            Car.Crush();
            other.GetComponent<CarCrashAnimation>()?.OnCrash(transform);
            ChunkManager.ReduceSpeedAfterCrush();
        }
    }
}