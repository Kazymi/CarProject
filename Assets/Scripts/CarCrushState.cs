using DG.Tweening;
using UnityEngine;

public class CarCrushState : State
{
    private WheelCarModule _wheelModule;
    private CarRotateModule _carRotateModule;
    private Transform _car;
    public bool IsFinish;

    public CarCrushState(WheelCarModule wheelModule, CarRotateModule carRotateModule, Transform car)
    {
        _wheelModule = wheelModule;
        _carRotateModule = carRotateModule;
        _car = car;
    }

    public override void Tick()
    {
        _carRotateModule.Tick();
    }

    public override void OnStateEnter()
    {
        _car.transform.DOLocalRotate(new Vector3(6, 0, 0), 0.4f).OnComplete(() =>
        {
            _car.transform.DOLocalRotate(Vector3.zero, 0.2f).OnComplete(() => { IsFinish = true; });
        });
        _wheelModule.StartWheel();
    }

    public override void OnStateExit()
    {
        IsFinish = false;
        _wheelModule.StopWheel();
    }
}