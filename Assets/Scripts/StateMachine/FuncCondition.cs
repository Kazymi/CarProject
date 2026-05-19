using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuncCondition : StateCondition
{
    private Func<bool> _condition;

    public override bool IsConditionSatisfied()
    {
        return _condition.Invoke();
    }

    public FuncCondition(Func<bool> condition)
    {
        _condition = condition;
    }
}