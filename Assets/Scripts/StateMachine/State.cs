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