using UnityEngine;

public class SecurityGuardPatrol0 : MonoBehaviour
{
    [Header("Patrol Speed")]
    public float speed = 2f;
    [Header("waypoints(followed by order)")]
    public Transform[] waypoints;
    
    // old, backup
    // public Transform leftPoint;   // Point A
    // public Transform rightPoint;  // Point B
    // private bool movingRight = true;
    
    private SpriteRenderer spriteRenderer;
    private Transform lightTransform;
    
    private Vector3 initialScale;
    private int currentIndex = 0; // current index
    private bool facingRight = true;  // if face right
    
    void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        lightTransform = transform.Find("LightObject");
        initialScale = transform.localScale;


        if (waypoints == null || waypoints.Length == 0)
        {
            Debug.LogError("add at least one waypoint");
            enabled = false;
            return;
        }

        // Start exactly at index 0
        transform.position = waypoints[0].position;
        Debug.Log("Guard starts at: " + transform.position);
        // old backup
        // transform.position = leftPoint.position;
        
        
    }

    void Update()
    {
        Patrol();
    }

    void Patrol()
    {
        Transform target = waypoints[currentIndex];
        Vector3 dir = (target.position - transform.position).normalized;

        // rotation when there is a new direction
        if (dir.sqrMagnitude > 0.001f)
        {
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            // rotate to target direction along Z axis
            transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }

        // then move
        transform.position = Vector2.MoveTowards(
            transform.position,
            target.position,
            speed * Time.deltaTime
        );

        // go next
        if (Vector2.Distance(transform.position, target.position) < 0.01f)
        {
            currentIndex = (currentIndex + 1) % waypoints.Length;
        }

        // If reached the target, flip direction
        // if (Vector2.Distance(transform.position, target.position) < 0.01f)
        // {
        //    movingRight = !movingRight;
        //    Flip();
       //  }
    }

    //void Flip()
    //{
        // Flip body
    //     Vector3 scale = transform.localScale;
    //     scale.x = Mathf.Abs(initialScale.x) * (facingRight ? -1 : 1);
    //      scale.x *= -1;
    //     transform.localScale = scale;

        // Flip light if needed
    //      if (lightTransform != null)
    //    {
     //       Vector3 lightScale = lightTransform.localScale;
     //       lightScale.x *= -1;
     //       lightTransform.localScale = lightScale;

     //       Vector3 lightRotation = lightTransform.eulerAngles;
     //       lightRotation.z = 180 - lightRotation.z;
     //      lightTransform.eulerAngles = lightRotation;
     //   }

        // Flip sprite
    //    if (spriteRenderer != null)
    //    {
    //       spriteRenderer.flipX = !spriteRenderer.flipX;
    //    }
    // }
}
