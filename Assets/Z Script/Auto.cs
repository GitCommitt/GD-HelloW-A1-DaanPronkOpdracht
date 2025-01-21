using UnityEngine;

public class AutoController : MonoBehaviour
{
    public float speed = 5f;

    public float turnSpeed = 5f;

    public float leftLimit = -8f;
    public float rightLimit = 8f;
    public float forwardLimit = 20f;

    public float detectionDistance = 3f;
    public LayerMask obstacleMask;

    private bool isMovingToTarget = false;
    private Vector3 targetPosition;

    void Start()
    {
        SetRandomTargetPosition();
    }

    void Update()
    {
        Vector3 position = transform.position;
        position.x = Mathf.Clamp(position.x, leftLimit, rightLimit);
        position.z = Mathf.Clamp(position.z, -Mathf.Infinity, forwardLimit);
        transform.position = position;

        if (isMovingToTarget)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                isMovingToTarget = false;
                SetRandomTargetPosition();
                isMovingToTarget = true;
            }

            AvoidObstacles();
        }
        else
        {
            isMovingToTarget = true;
        }

        if (isMovingToTarget)
        {
            Vector3 targetDirection = (targetPosition - transform.position).normalized;
            float step = turnSpeed * Time.deltaTime;
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, step, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDirection);
        }
    }

    private void SetRandomTargetPosition()
    {
        float randomX = Random.Range(leftLimit, rightLimit);
        float randomZ = Random.Range(0f, forwardLimit);
        targetPosition = new Vector3(randomX, transform.position.y, randomZ);
    }

    private void AvoidObstacles()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, detectionDistance, obstacleMask))
        {
            SetRandomTargetPosition();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * detectionDistance);
    }
}
