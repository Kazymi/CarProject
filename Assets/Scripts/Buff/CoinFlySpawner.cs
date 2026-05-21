using DG.Tweening;
using UnityEngine;

public class CoinFlySpawner : MonoBehaviour
{
    public ChunkManager ChunkManager;
    public Transform[] spawnCoinPosition;
    public GameObject CoinPrefab;
    public float SpawnInterval;

    private bool canSpawn = true;

    private int spawnLineId;
    private int cointInLine;

    public void SpawnCoin()
    {
        if (canSpawn == false) return;
        if (cointInLine == 0)
        {
            spawnLineId = Random.Range(0, spawnCoinPosition.Length);
            cointInLine = Random.Range(4, 10);
        }

        canSpawn = false;
        DOVirtual.DelayedCall(SpawnInterval, () => { canSpawn = true; });

        GameObject newCoin = Instantiate(CoinPrefab, spawnCoinPosition[spawnLineId].position, Quaternion.identity);
        MonoPooled lastChunk = ChunkManager.GetLastChunk();
        newCoin.transform.parent = lastChunk.transform;
        Destroy(newCoin.gameObject, 5);
        cointInLine--;
    }
}