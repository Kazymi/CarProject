using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoPooled
{
    public List<GameObject> spawnObjects;
    public List<Transform> spawnPositions;

    private List<GameObject> spawnedObjects = new List<GameObject>();
    
    public void ChunkSpawned(int amountObjects)
    {
        for (int i = 0; i < amountObjects; i++)
        {
            GameObject randomObject = spawnObjects[Random.Range(0, spawnObjects.Count)];
            Transform spawnPosition = spawnPositions[Random.Range(0, spawnPositions.Count)];
            GameObject newObject = Instantiate(randomObject, spawnPosition);
            newObject.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            spawnedObjects.Add(newObject);
        }
    }

    public override void ReturnToPool()
    {
        base.ReturnToPool();
        foreach (var spawnedObject in spawnedObjects)
        {
            Destroy(spawnedObject.gameObject);
        }
        spawnedObjects.Clear();
    }
}
