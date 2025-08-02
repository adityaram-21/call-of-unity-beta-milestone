using UnityEngine;

public class TriangleVisionBlocker : MonoBehaviour
{
    public Transform triangle;              // Assign in inspector (child triangle)
    public LayerMask wallLayer;             // Assign 'Wall' layer in inspector
    public float maxLength = 5f;            // Maximum vision cone length

    void Update()
    {
        // Starting point is triangle's own position
        Vector2 origin = triangle.position;
        Vector2 direction = triangle.right; // triangle's own forward

        // Cast ray to detect walls
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, maxLength, wallLayer);

        float newLength = maxLength;
        if (hit.collider != null)
        {
            newLength = hit.distance;
        }

        // Scale triangle's X based on hit
        Vector3 scale = triangle.localScale;
        scale.x = newLength;
        triangle.localScale = scale;
    }
}
