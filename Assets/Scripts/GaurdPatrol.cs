using UnityEngine;

public class SecurityGuardPatrol : MonoBehaviour
{
    public float speed = 2f;
    public Transform leftPoint;   // Point A
    public Transform rightPoint;  // Point B

    private bool movingRight = true;
    private SpriteRenderer spriteRenderer;
    private Transform lightTransform;

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

        // If reached the target, flip direction
        if (Vector2.Distance(transform.position, target.position) < 0.01f)
        {
            movingRight = !movingRight;
            Flip();
        }
    }

    void Flip()
    {
        // Flip body
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;

        // Flip light if needed
        if (lightTransform != null)
        {
            Vector3 lightScale = lightTransform.localScale;
            lightScale.x *= -1;
            lightTransform.localScale = lightScale;

            Vector3 lightRotation = lightTransform.eulerAngles;
            lightRotation.z = 180 - lightRotation.z;
            lightTransform.eulerAngles = lightRotation;
        }

        // Flip sprite
        if (spriteRenderer != null)
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }
    }
}
