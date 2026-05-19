public interface ITransitionState
{
    public State StateTo { get; }
    public StateCondition Condition { get; }
    public void Initialize();
    public void DeInitialize();
}