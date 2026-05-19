using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State
{
    public List<ITransitionState> Transitions { get; private set; } = new List<ITransitionState>();

    public virtual void Tick()
    {
    }

    public virtual void FixedTick()
    {
    }

    public virtual void OnStateEnter()
    {
    }

    public virtual void OnStateExit()
    {
    }

    public void AddTransition(ITransitionState transition)
    {
        Transitions.Add(transition);
    }

    public void RemoveTransition(ITransitionState transition)
    {
        if (Transitions.Contains(transition))
        {
            Transitions.Remove(transition);
        }
    }

    public void InitializeTransitions()
    {
        foreach (ITransitionState transition in Transitions)
        {
            transition.Initialize();
        }
    }

    public void DeInitializeTransitions()
    {
        foreach (ITransitionState transition in Transitions)
        {
            transition.DeInitialize();
            if (Transitions.Contains(transition) == false)
            {
                DeInitializeTransitions();
                return;
            }
        }
    }
}

public class StateTransition : ITransitionState
{
    public State StateTo { get; private set; }
    public StateCondition Condition { get; private set; }
    public event Action OnTransitionDeInitialized;

    public StateTransition(State stateTo, StateCondition condition)
    {
        StateTo = stateTo;
        Condition = condition;
    }

    public void Initialize()
    {
        Condition.Initialize();
    }

    public void DeInitialize()
    {
        Condition.DeInitialize();
        OnTransitionDeInitialized?.Invoke();
    }
}

public interface ITransitionState
{
    public State StateTo { get; }
    public StateCondition Condition { get; }
    public void Initialize();
    public void DeInitialize();
}

public abstract class StateCondition
{
    public abstract bool IsConditionSatisfied();

    public virtual void Tick()
    {
    }

    public virtual void Initialize()
    {
    }

    public virtual void DeInitialize()
    {
    }
}

public class StateMachine
{
    public State CurrentState { get; private set; }

    public StateMachine(State state)
    {
        SetState(state);
    }

    public void Tick()
    {
        int currentTransition = IsTransitionsCondition();
        if (currentTransition == -1)
        {
            CurrentState.Tick();
        }
        else
        {
            SetState(CurrentState.Transitions[currentTransition].StateTo);
        }
    }

    public void FixedTick()
    {
        CurrentState.FixedTick();
    }

    public void SetState(State nextState)
    {
        CurrentState?.DeInitializeTransitions();
        CurrentState?.OnStateExit();
        
        CurrentState = nextState;
        CurrentState.OnStateEnter();
        CurrentState.InitializeTransitions();
    }

    private int IsTransitionsCondition()
    {
        List<ITransitionState> currentList = CurrentState.Transitions;
        for (int i = 0; i < currentList.Count; i++)
        {
            StateCondition currentCondition = currentList[i].Condition;
            currentCondition.Tick();
            if (currentCondition.IsConditionSatisfied())
            {
                return i;
            }
        }

        return -1;
    }
}