using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMovement3D : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float rotationSpeed = 200f;
    public float moveRadius = 5f;
    public float waitTime = 2f;

    private Vector3 targetPosition;
    private float waitTimer;
    private bool isRotating = false;

    void Start()
    {
        SetNewRandomTarget();
        waitTimer = waitTime;
    }

    void Update()
    {
        if (isRotating)
        {
            RotateTowardsTarget();
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                waitTimer -= Time.deltaTime;

                if (waitTimer <= 0f)
                {
                    SetNewRandomTarget();
                    waitTimer = waitTime;
                }
            }
        }
    }

    void SetNewRandomTarget()
    {
        float randomX = Random.Range(-moveRadius, moveRadius) + transform.position.x;
        float randomZ = Random.Range(-moveRadius, moveRadius) + transform.position.z;
        targetPosition = new Vector3(randomX, transform.position.y, randomZ);

        isRotating = true;
    }

    void RotateTowardsTarget()
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        if (Quaternion.Angle(transform.rotation, targetRotation) < 1f)
        {
            isRotating = false;
        }
    }
}
