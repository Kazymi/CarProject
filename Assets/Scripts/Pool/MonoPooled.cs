using System;
using UnityEngine;

public class MonoPooled : MonoBehaviour, IPooledObject
{
    private IPool _pool;

    public Action ReturnToPoolEvent { get; set; }

    public virtual void ReturnToPool()
    {
        ReturnToPoolEvent?.Invoke();
        gameObject.SetActive(false);
        _pool.Push(this);
    }

    public void Initialize()
    {
        gameObject.SetActive(true);
    }

    public void SetPool<T>(IPool<T> poolParent) where T : IPooledObject
    {
        _pool = poolParent;
    }
}