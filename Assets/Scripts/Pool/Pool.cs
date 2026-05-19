using System.Collections.Generic;

public class Pool<T> : IPool<T> where T : IPooledObject
{
    private List<T> _pooledObjects = new List<T>();
    private IFactory<T> _factory;

    public Pool(IFactory<T> factory)
    {
        _factory = factory;
    }

    public T Pull()
    {
        if (_pooledObjects.Count == 0)
        {
            T NewObjcet = _factory.CreteObject();
            NewObjcet.SetPool(this);
            _pooledObjects.Add(NewObjcet);
        }

        T returnValue = _pooledObjects[0];
        returnValue.Initialize();
        _pooledObjects.Remove(returnValue);
        return returnValue;
    }

    public void Push(IPooledObject pooledObject)
    {
        _pooledObjects.Add((T)pooledObject);
    }
}