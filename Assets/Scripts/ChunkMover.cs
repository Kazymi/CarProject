using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkMover : MonoBehaviour
{
    public float MoveSpeed;
    public float MinX;
    public float MinY;

    public float Direction;

    private void Update()
    {
        float dir = 0;
        if (Input.GetKey(KeyCode.D))
        {
            dir = -1;
        }

        if (Input.GetKey(KeyCode.A))
        {
            dir = 1;
        }

        Vector3 position = transform.position;
        float previousX = position.x;
        position.x += dir * MoveSpeed * Time.deltaTime;
        position.x = Mathf.Clamp(position.x, MinX, MinY);
        transform.position = position;
        Direction = Mathf.Abs(position.x - previousX) > 0.001f ? dir : 0;
    }
}