using DG.Tweening;
using UnityEngine;

public class FlyBuff : IBuff
{
    private IMultiplierSpeedForBuff _multiplierSpeedForBuff;
    private Transform _car;
    private CoinFlySpawner _coinFlySpawner;
    private Transform _camera;
    public int Duration => 10;

    private const float SpeedMultiplier = 0.5f;

    public FlyBuff(IMultiplierSpeedForBuff multiplierSpeedForBuff, Transform car, CoinFlySpawner coinFlySpawner, Transform camera)
    {
        _multiplierSpeedForBuff = multiplierSpeedForBuff;
        _car = car;
        _coinFlySpawner = coinFlySpawner;
        _camera = camera;
    }

    public void StartBuff()
    {
        _camera.transform.DOLocalMove(_camera.transform.localPosition + new Vector3(0, 10, 0), 1f);
        _car.transform.DOLocalMove(_car.transform.localPosition + new Vector3(0, 10, 0), 1f);
        _multiplierSpeedForBuff.AddSpeedMultiplier(SpeedMultiplier);
    }

    public void EndBuff()
    {
        _camera.transform.DOLocalMove(_camera.transform.localPosition - new Vector3(0, 10, 0), 1f);
        _car.transform.DOLocalMove(_car.transform.localPosition - new Vector3(0, 10, 0), 1f);
        _multiplierSpeedForBuff.RemoveSpeedMultiplier(SpeedMultiplier);
    }

    public void Tick()
    {
        _coinFlySpawner.SpawnCoin();
    }
}