using System;

public interface IPooledObject
{
    public Action ReturnToPoolEvent { get; set; }
    void ReturnToPool();
    void Initialize();
    void SetPool<T>(IPool<T> poolParent) where T : IPooledObject;
}