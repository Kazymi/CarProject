using System.Collections.Generic;

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