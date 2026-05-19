using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class Car : MonoBehaviour
{
    public Transform[] _wheels;
    public Transform[] _rotatedWheels;
    public ChunkManager _chunkManager;
    public ChunkMover _chunkMover;
    public WheelRotateModule _wheelRotateModule;

    private StateMachine _stateMachine;
    private WheelCarModule _wheelModule;
    private CarRotateModule _carRotateModule;

    private void Start()
    {
        InitializeStateMachine();
    }

    private void InitializeStateMachine()
    {
        _wheelModule = new WheelCarModule(_chunkManager, _wheels);
        _carRotateModule = new CarRotateModule(_chunkMover, transform);
        _wheelRotateModule = new WheelRotateModule(_chunkMover, _rotatedWheels);
        State idleState = new IdleStataForCar(transform);
        State runState = new RunStateForCar(_wheelModule, _carRotateModule, transform);

        idleState.AddTransition(new StateTransition(runState,
            new FuncCondition(() => _chunkManager.CurrentSpeed != 0)));
        _stateMachine = new StateMachine(idleState);
    }

    private void Update()
    {
        _stateMachine.Tick();
        _wheelModule.Tick();
        _wheelRotateModule.Tick();
    }
}

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

public class WheelCarModule
{
    private ChunkManager _chunkManager;
    private Transform[] _wheels;

    private bool _isWheelRotate;
    private float _currentSpeedModification;

    public WheelCarModule(ChunkManager chunkManager, Transform[] wheels)
    {
        _chunkManager = chunkManager;
        _wheels = wheels;
    }

    public void Tick()
    {
        if (_isWheelRotate)
        {
            _currentSpeedModification += Time.deltaTime;
        }
        else
        {
            _currentSpeedModification -= Time.deltaTime;
        }

        _currentSpeedModification = Mathf.Clamp(_currentSpeedModification, 0f, 1f);
        float currentSpeed = _chunkManager.CurrentSpeed * _currentSpeedModification;
        foreach (var wheel in _wheels)
        {
            wheel.transform.Rotate(currentSpeed, 0, 0, Space.Self);
        }
    }

    public void StartWheel()
    {
        _isWheelRotate = true;
    }

    public void StopWheel()
    {
        _isWheelRotate = false;
    }
}

public class CarRotateModule
{
    private ChunkMover _chunkMover;
    private Transform _car;

    private const float Angle = 8f;
    private const float Speed = 8f;

    public CarRotateModule(ChunkMover chunkMover, Transform car)
    {
        _chunkMover = chunkMover;
        _car = car;
    }

    public void Tick()
    {
        float direction = -_chunkMover.Direction;
        Quaternion targetRotation = Quaternion.Euler(0f, direction * Angle, 0f);
        _car.transform.localRotation =
            Quaternion.Lerp(_car.transform.localRotation, targetRotation, Speed * Time.deltaTime);
    }
}

public class RunStateForCar : State
{
    private WheelCarModule _wheelModule;
    private CarRotateModule _carRotateModule;
    private Transform _car;

    public RunStateForCar(WheelCarModule wheelModule, CarRotateModule carRotateModule, Transform car)
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
        _car.transform.DOLocalRotate(new Vector3(-6, 0, 0), 0.4f).OnComplete(() =>
        {
            _car.transform.DOLocalRotate(Vector3.zero, 0.2f);
        });
        _wheelModule.StartWheel();
    }

    public override void OnStateExit()
    {
        _wheelModule.StopWheel();
    }
}