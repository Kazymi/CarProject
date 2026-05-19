using UnityEngine;

public class CarRotateModule
{
    private ChunkMover _chunkMover;
    private Transform _car;

    private const float Angle = 8f;
    private const float Speed = 8f;

    public CarRotateModule(ChunkMover chunkMover, Transform car)
    {
        _chunkMover = chunkMover;
        _car = car;
    }

    public void Tick()
    {
        float direction = -_chunkMover.Direction;
        Quaternion targetRotation = Quaternion.Euler(0f, direction * Angle, 0f);
        _car.transform.localRotation =
            Quaternion.Lerp(_car.transform.localRotation, targetRotation, Speed * Time.deltaTime);
    }
}