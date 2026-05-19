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