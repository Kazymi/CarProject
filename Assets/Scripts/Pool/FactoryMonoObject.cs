using System;
using System.Collections;
using UnityEngine;

public class FactoryMonoObject<T> : IFactory<T> where T : MonoBehaviour
{
    private T _prefab;
    private Transform _factoryParent;

    public FactoryMonoObject(T prefab, Transform factoryParent)
    {
        _prefab = prefab;
        _factoryParent = factoryParent;
    }

    public T CreteObject()
    {
        T newObject = GameObject.Instantiate(_prefab.gameObject, _factoryParent).GetComponent<T>();
        newObject.gameObject.SetActive(false);
        return newObject;
    }
}


public interface IPool
{
    void Push(IPooledObject pooledObject);
}