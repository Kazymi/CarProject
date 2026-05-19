using UnityEngine;

public class ObjectSideMover : MonoBehaviour
{
    public float MoveSpeed = 5f;
    public float MinX = -3f;
    public float MaxX = 3f;
    public Transform CarTransform;
    public float TurnAngle = 15f;
    public float TurnSpeed = 8f;
    public Transform[] SteeringWheels;
    public float WheelTurnAngle = 25f;

    private Quaternion[] _startWheelRotations;

    private void Awake()
    {
        _startWheelRotations = new Quaternion[SteeringWheels.Length];

        for (int i = 0; i < SteeringWheels.Length; i++)
        {
            if (SteeringWheels[i] != null)
            {
                _startWheelRotations[i] = SteeringWheels[i].localRotation;
            }
        }
    }

    private void Update()
    {
        float direction = 0f;

        if (Input.GetKey(KeyCode.A))
        {
            direction = 1f;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            direction = -1f;
        }

        Vector3 position = transform.position;
        float previousX = position.x;
        position.x += direction * MoveSpeed * Time.deltaTime;
        position.x = Mathf.Clamp(position.x, MinX, MaxX);
        transform.position = position;

        float movementDirection = Mathf.Abs(position.x - previousX) > 0.001f ? direction : 0f;
        RotateCar(movementDirection);
        RotateWheels(movementDirection);
    }

    private void RotateCar(float direction)
    {
        direction = -direction;
        if (CarTransform == null)
        {
            return;
        }

        Quaternion targetRotation = Quaternion.Euler(0f, direction * TurnAngle, 0f);
        CarTransform.localRotation = Quaternion.Lerp(
            CarTransform.localRotation,
            targetRotation,
            TurnSpeed * Time.deltaTime);
    }

    private void RotateWheels(float direction)
    {
        direction = -direction;

        for (int i = 0; i < SteeringWheels.Length; i++)
        {
            if (SteeringWheels[i] == null)
            {
                continue;
            }

            Quaternion targetRotation = _startWheelRotations[i] * Quaternion.Euler(0f, direction * WheelTurnAngle, 0f);
            SteeringWheels[i].localRotation = Quaternion.Lerp(
                SteeringWheels[i].localRotation,
                targetRotation,
                TurnSpeed * Time.deltaTime);
        }
    }
}
