using DG.Tweening;
using UnityEngine;

public class IdleStataForCar : State
{
    private Transform _car;
    private Sequence _carSequence;
    private Vector3 _startScale;

    public IdleStataForCar(Transform car)
    {
        _car = car;
        _startScale = _car.localScale;
    }

    public override void OnStateEnter()
    {
        _carSequence = DOTween.Sequence();
        _carSequence.Append(_car.DOShakeScale(0.2f, 0.01f));
        _carSequence.SetLoops(-1, LoopType.Yoyo);
    }

    public override void OnStateExit()
    {
        _carSequence.Kill();
        _car.transform.localScale = _startScale;
    }
}