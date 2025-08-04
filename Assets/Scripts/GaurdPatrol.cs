using UnityEngine;

public class SecurityGuardPatrol : MonoBehaviour
{
    public float speed = 2f;
    public Transform leftPoint;
    public Transform rightPoint;
    public Transform player;
    public float chaseTime = 5f; // Duration to chase the player
    public float returnDelay = 2f; // Wait before returning to patrol

    private bool movingRight = true;
    private Vector3 originalPosition;
    private float chaseTimer;
    private float returnTimer;

    private enum GuardState { Patrolling, Chasing, Returning }
    private GuardState currentState = GuardState.Patrolling;

    private SpriteRenderer spriteRenderer;
    private Transform lightTransform;

    void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        lightTransform = transform.Find("LightObject");
        transform.position = leftPoint.position;
        originalPosition = transform.position;
    }

    void Update()
    {
        switch (currentState)
        {
            case GuardState.Patrolling:
                Patrol();
                break;
            case GuardState.Chasing:
                ChasePlayer();
                break;
            case GuardState.Returning:
                ReturnToPatrol();
                break;
        }
    }

    void Patrol()
    {
        Transform target = movingRight ? rightPoint : leftPoint;
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, target.position) < 0.01f)
        {
            movingRight = !movingRight;
            Flip(movingRight);
        }
    }

    void ChasePlayer()
    {
        if (player == null) return;

        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * 1.5f * Time.deltaTime);
        Flip(transform.position.x < player.position.x); // face direction of movement

        chaseTimer -= Time.deltaTime;

        if (chaseTimer <= 0f)
        {
            currentState = GuardState.Returning;
            returnTimer = returnDelay;
        }
    }

    void ReturnToPatrol()
    {
        transform.position = Vector2.MoveTowards(transform.position, originalPosition, speed * Time.deltaTime);
        Flip(transform.position.x < originalPosition.x);

        returnTimer -= Time.deltaTime;

        if (Vector2.Distance(transform.position, originalPosition) < 0.1f && returnTimer <= 0f)
        {
            transform.position = originalPosition;
            currentState = GuardState.Patrolling;
        }
    }

    void Flip(bool faceRight)
    {
        float targetZ = faceRight ? -90f : 90f;
        transform.localRotation = Quaternion.Euler(0f, 0f, targetZ);
    }

    // Called by your FlashlightDetector script on detection
    public void OnPlayerDetected()
    {
        chaseTimer = chaseTime;
        currentState = GuardState.Chasing;
    }
}
