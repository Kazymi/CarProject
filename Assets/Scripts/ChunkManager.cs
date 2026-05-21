using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class ChunkManager : MonoBehaviour, ISpeedForBuff, IMultiplierSpeedForBuff
{
    public Transform CameraTransform;
    public List<MonoPooled> Chunks = new List<MonoPooled>();

    public int InitialBlockCount = 8;
    public float BlockLength = 10;

    public float StartMoveSpeed = 10;
    public float MaxSpeed = 30f;
    public float SpeedIncreasePerSecond = 0.4f;

    public float recycleDistanceBehindCamera = 15;

    private float speedMultiplier;
    private int amountSpawnedChunk;
    private List<Pool<MonoPooled>> lastChunks = new List<Pool<MonoPooled>>();
    private List<MonoPooled> _activeChunks = new List<MonoPooled>();
    private float _currentSpeed = 0;
    private List<Pool<MonoPooled>> _pools = new List<Pool<MonoPooled>>();

    public float CurrentSpeed => _currentSpeed;

    private void Awake()
    {
        foreach (var chunk in Chunks)
        {
            FactoryMonoObject<MonoPooled> factoryMonoObject = new FactoryMonoObject<MonoPooled>(chunk, transform);
            _pools.Add(new Pool<MonoPooled>(factoryMonoObject));
        }

        SpawnInitialChunks();
    }

    public MonoPooled GetLastChunk()
    {
        return _activeChunks.Last();
    }
    
    private void Update()
    {
        RecalculateSpeed();
        MoveBlocks(_currentSpeed);
        RecycleBlockPassedCamera();
    }

    private void RecalculateSpeed()
    {
        if (_currentSpeed < StartMoveSpeed)
        {
            _currentSpeed += StartMoveSpeed * Time.deltaTime;
            if (_currentSpeed > StartMoveSpeed)
            {
                _currentSpeed = StartMoveSpeed;
            }
        }
        else
        {
            if (_currentSpeed != MaxSpeed)
            {
                _currentSpeed += SpeedIncreasePerSecond * Time.deltaTime;
            }
        }

        if (_currentSpeed > MaxSpeed)
        {
            _currentSpeed = MaxSpeed;
        }
    }

    private void SpawnInitialChunks()
    {
        float nextSpawnPositionZ = CameraTransform.position.z;
        for (int i = 0; i < InitialBlockCount; i++)
        {
            MonoPooled spawnedChunk = InstantiateChunk(nextSpawnPositionZ);
            _activeChunks.Add(spawnedChunk);
            nextSpawnPositionZ += BlockLength;
        }
    }

    private Pool<MonoPooled> GetChunk()
    {
        if (lastChunks.Count >= 2)
        {
            var randomChunk = _pools[Random.Range(0, _pools.Count)];
            while (lastChunks.Contains(randomChunk))
            {
                randomChunk = _pools[Random.Range(0, Chunks.Count)];
            }

            return randomChunk;
        }

        return _pools[Random.Range(0, Chunks.Count)];
    }

    private void MoveBlocks(float moveSpeed)
    {
        float moveDistance = moveSpeed * Time.deltaTime;
        moveDistance += moveDistance * speedMultiplier;
        Vector3 moveOffset = new Vector3(0, 0, -moveDistance);
        foreach (var activeChunk in _activeChunks)
        {
            activeChunk.transform.position += moveOffset;
        }
    }

    public void ReduceSpeedAfterCrush()
    {
        _currentSpeed *= 0.5f;
        if (_currentSpeed < StartMoveSpeed)
        {
            Debug.Log("Lose");
        }
    }

    private void RecycleBlockPassedCamera()
    {
        float recycleThreshold = CameraTransform.position.z - recycleDistanceBehindCamera;
        while (_activeChunks.Count > 0)
        {
            MonoPooled oldestBlock = _activeChunks[0];
            if (oldestBlock.transform.position.z >= recycleThreshold)
            {
                return;
            }

            MonoPooled recycleChunk = _activeChunks[0];
            _activeChunks.Remove(recycleChunk);
            float frontBlockZPosition =
                _activeChunks.Count == 0 ? recycleChunk.transform.position.z : GetFrontPositionZ();
            float nextBlockZPosition = frontBlockZPosition + BlockLength;
            oldestBlock.ReturnToPool();
            MonoPooled newBlock = InstantiateChunk(nextBlockZPosition);
            _activeChunks.Add(newBlock);
        }
    }

    private float GetFrontPositionZ()
    {
        float returnValue = float.MinValue;
        foreach (var activeChunk in _activeChunks)
        {
            if (activeChunk.transform.position.z > returnValue)
            {
                returnValue = activeChunk.transform.position.z;
            }
        }

        return returnValue;
    }

    private MonoPooled InstantiateChunk(float zPosition)
    {
        Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y, zPosition);
        Pool<MonoPooled> lastChunk = GetChunk();
        MonoPooled newChunk = lastChunk.Pull();

        amountSpawnedChunk++;
        switch (amountSpawnedChunk)
        {
            case < 10: newChunk.GetComponent<Chunk>().ChunkSpawned(Random.Range(0, 1)); break;
            case < 50: newChunk.GetComponent<Chunk>().ChunkSpawned(Random.Range(0, 2)); break;
            case > 70: newChunk.GetComponent<Chunk>().ChunkSpawned(2); break;
        }

        if (lastChunks.Count >= 2)
        {
            lastChunks.Remove(lastChunks.First());
        }

        lastChunks.Add(lastChunk);
        newChunk.transform.position = spawnPosition;
        newChunk.transform.SetParent(transform);
        return newChunk;
    }

    public void AddSpeed(float speed)
    {
        _currentSpeed += speed;
    }

    public void RemoveSpeed(float speed)
    {
        _currentSpeed -= speed;
        if (_currentSpeed < StartMoveSpeed) _currentSpeed = StartMoveSpeed;
    }

    public void AddSpeedMultiplier(float speedMultiplier)
    {
        this.speedMultiplier += speedMultiplier;
    }

    public void RemoveSpeedMultiplier(float speedMultiplier)
    {
        this.speedMultiplier -= speedMultiplier;
    }
}