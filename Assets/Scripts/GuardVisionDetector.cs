using UnityEngine;
using UnityEngine.Rendering.Universal;
#if UNITY_EDITOR
using UnityEditor;  // use Handles.DrawWireArc only in editor
#endif

public class GuardVisionDetector : MonoBehaviour
{
    [Header("Light2D Vision")]
    public Light2D guardLight; // drag Guard's Spot Light
    public Transform player; // drag Player.Transform

    [Header("Layer Masks")]
    public LayerMask obstacleMask; // only choose wall layer
    public LayerMask playerMask; // only choose Player layer

    float radius;
    float halfAngle;

    void Start()
    {
        if (guardLight == null || player == null)
        {
            Debug.LogError("drag guardLight and player in Inspector fisrt！");
            enabled = false;
            return;
        }

        // cache
        radius = guardLight.pointLightOuterRadius;
        halfAngle = guardLight.pointLightOuterAngle * 0.5f;
    }

    void Update()
    {
        Vector2 origin= guardLight.transform.position;
        Vector2 playerPos= player.position;
        Vector2 dir = playerPos - origin;
        float dist = dir.magnitude;

        // give up when over radius
        if (dist > radius) return;

        // give up when over angle
        float ang = Vector2.Angle(guardLight.transform.up, dir);
        if (ang > halfAngle) return;

        // ray detection for if there is a wall 
        var hit = Physics2D.Raycast(origin, dir.normalized, dist, obstacleMask | playerMask);
        if (hit.collider != null && ((playerMask.value & (1 << hit.collider.gameObject.layer)) != 0))
        {
            Debug.Log("Guard spotted the player!");
            
            GameManager gm = FindObjectOfType<GameManager>();
            if (gm != null)
            {
                gm.GameOver("Caught by guard’s light!");
            }
        }
    }
    
    void OnCollisionEnter2D(Collision2D col)
    {
        if (playerMask == (playerMask | (1 << col.gameObject.layer)))
        {
            Fail("Caught by guard!");
        }
    }

    void Fail(string message)
    {
        Time.timeScale = 0f;
        var gm = FindObjectOfType<GameManager>();
        if (gm != null)
            gm.GameOver(message);
    }

    // debug only
    void OnDrawGizmosSelected()
    {
        if (guardLight == null) return;
        Gizmos.color = Color.yellow;

        Vector3 origin = guardLight.transform.position;
        Vector3 forward = guardLight.transform.up;
        float angle = guardLight.pointLightOuterAngle;
        float halfA = angle * 0.5f;
        float rad = guardLight.pointLightOuterRadius;

        // two edges
        Vector3 dirL = Quaternion.Euler(0, 0, -halfA) * forward * rad;
        Vector3 dirR = Quaternion.Euler(0, 0, +halfA) * forward * rad;
        Gizmos.DrawLine(origin, origin + dirL);
        Gizmos.DrawLine(origin, origin + dirR);

        // draw area
#if UNITY_EDITOR
        Handles.DrawWireArc(origin, Vector3.forward, dirL.normalized, angle, rad);
#endif
    }
}
