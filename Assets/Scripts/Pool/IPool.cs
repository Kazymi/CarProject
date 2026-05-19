public interface IPool<T> : IPool where T : IPooledObject
{
    T Pull();
}