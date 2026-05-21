using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BuffSystem : MonoBehaviour
{
    public CarTrigger CarTrigger;
    public ChunkManager ChunkManager;
    public CoinFlySpawner FlySpawner;

    private List<IBuff> _buffs = new List<IBuff>();

    private void Update()
    {
        foreach (var buff in _buffs)
        {
            buff.Tick();
        }
    }

    public void AddBuff(IBuff buff)
    {
        buff.StartBuff();
        _buffs.Add(buff);
        DOVirtual.DelayedCall(buff.Duration, () => { RemoveBuff(buff); });
    }

    public void RemoveBuff(IBuff buff)
    {
        buff.EndBuff();
        _buffs.Remove(buff);
    }

    public void AddFlyBuff()
    {
        AddBuff(new FlyBuff(ChunkManager, CarTrigger.transform, FlySpawner, Camera.main.transform));
    }

    public void AddSpeedBuff()
    {
        
    }
}