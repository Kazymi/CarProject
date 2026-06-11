using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarTrigger : MonoBehaviour, IInvisibleForBuff
{
    public ChunkManager ChunkManager;
    public Car Car;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Car")
        {
            CrushVolumeChanger.Instance.Crush();
            Car.Crush();
            other.GetComponent<CarCrashAnimation>()?.OnCrash(transform);
            ChunkManager.ReduceSpeedAfterCrush();
        }
    }

    public void SetInvisible(bool invisible)
    {
        GetComponent<Collider>().enabled = !invisible;
    }
}