using UnityEngine;

public class WheelRotateModule
{
    private ChunkMover _chunkMover;
    private Transform[] _wheel;

    private const float Angle = 25f;
    private const float Speed = 8f;

    public WheelRotateModule(ChunkMover chunkMover, Transform[] wheel)
    {
        _chunkMover = chunkMover;
        _wheel = wheel;
    }

    public void Tick()
    {
        float direction = -_chunkMover.Direction;
        Quaternion targetRotation = Quaternion.Euler(0f, direction * Angle, 0f);
        foreach (var wheel in _wheel)
        {
            wheel.transform.localRotation =
                Quaternion.Lerp(wheel.transform.localRotation, targetRotation, Speed * Time.deltaTime);    
        }
        
    }
}