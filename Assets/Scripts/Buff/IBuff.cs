public interface IBuff
{
    int Duration { get; }
    void StartBuff();
    void EndBuff();
    void Tick();
}