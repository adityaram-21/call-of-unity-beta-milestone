using UnityEngine;

public class SecurityGuardPatrol : MonoBehaviour
{
    public float speed = 2f;
    public Transform leftPoint;   // Point A
    public Transform rightPoint;  // Point B

    private bool movingRight = true;
    private SpriteRenderer spriteRenderer;
    private Transform lightTransform;

    private bool hasStartedPatrol = false;

    void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        lightTransform = transform.Find("LightObject");

        if (leftPoint == null || rightPoint == null)
        {
            Debug.LogError("LeftPoint or RightPoint not assigned.");
            return;
        }

        // Start exactly at leftPoint
        transform.position = leftPoint.position;
        Debug.Log("Guard starts at: " + transform.position);
    }

    void Update()
    {
        Patrol();
    }

    void Patrol()
{
    Transform target = movingRight ? rightPoint : leftPoint;

    // Move towards target
    transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

    // If reached the target
    if (Vector2.Distance(transform.position, target.position) < 0.01f)
    {
        movingRight = !movingRight;

        // Explicitly face direction
        Flip(movingRight);  // face right when moving right, left when moving left
    }
}



    void Flip(bool faceRight)
{
    // Flip the whole diamond to face correct direction (0 = right, 180 = left)
    float targetZ = faceRight ? -90f : 90f;
    transform.localRotation = Quaternion.Euler(0f, 0f, targetZ);
}


    


}
