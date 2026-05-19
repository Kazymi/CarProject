using UnityEngine;

public class MonoPooled : MonoBehaviour, IPooledObject
{
    private IPool _pool;

    public virtual void ReturnToPool()
    {
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