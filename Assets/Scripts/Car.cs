using System;
using System.Collections;
using System.Collections.Generic;
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
    private CarCrushState _crushState;

    private void Start()
    {
        InitializeStateMachine();
    }

    public void Crush()
    {
        _stateMachine.SetState(_crushState);
    }

    private void InitializeStateMachine()
    {
        _wheelModule = new WheelCarModule(_chunkManager, _wheels);
        _carRotateModule = new CarRotateModule(_chunkMover, transform);
        _wheelRotateModule = new WheelRotateModule(_chunkMover, _rotatedWheels);
        State idleState = new IdleStataForCar(transform);
        State runState = new RunStateForCar(_wheelModule, _carRotateModule, transform);
        _crushState = new CarCrushState(_wheelModule, _carRotateModule, transform);

        idleState.AddTransition(new StateTransition(runState,
            new FuncCondition(() => _chunkManager.CurrentSpeed != 0)));
        _crushState.AddTransition(new StateTransition(runState, new FuncCondition(() => _crushState.IsFinish)));

        _stateMachine = new StateMachine(idleState);
    }

    private void Update()
    {
        _stateMachine.Tick();
        _wheelModule.Tick();
        _wheelRotateModule.Tick();
    }
}