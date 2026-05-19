public interface IPooledObject
{
    void ReturnToPool();
    void Initialize();
    void SetPool<T>(IPool<T> poolParent) where T : IPooledObject;
}