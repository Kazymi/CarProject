using System;

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