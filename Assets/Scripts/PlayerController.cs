using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Move")]
    public float moveSpeed = 5.0f;

    private Rigidbody2D rb;
    private Vector2 moveInput;

    [Header("Camera")]
    public Camera mainCamera;

    [Header("Flashlight")]
    public GameObject flashlight;
    private bool flashlightOn = true;
    // distance check, use for interaction
    public float maxInteractDistance = 3f;

    [Header("Battery Settings")]
    public float batteryLife = 90f; // Total battery life
    public float batteryDrainRate = 1.5f; // Battery drain rate per second when flashlight is on
    public float batteryRechargeRate = 3f; // Battery recharge rate per second when flashlight is off
    private float currentBatteryLife;

    [Header("Battery Display")]
    public RectTransform fillBar; // Assign the Fill image's RectTransform
    public Image fillImage;       // The Fill Image (for color change)
    public float maxWidth = 200f;
    public TMP_Text batteryText; // Text to display battery percentage

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
        currentBatteryLife = batteryLife;
        // Initialize flashlight state
        flashlightOn = true;
        if (flashlight != null)
        {
            flashlight.SetActive(flashlightOn);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // WASD input
        moveInput.x = Input.GetAxisRaw("Horizontal"); // A D
        moveInput.y = Input.GetAxisRaw("Vertical");   // W S
        moveInput.Normalize();

        // Rotation with Mouse
        Vector3 mouseWorld = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dir = (mouseWorld - transform.position);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f; // direction
        rb.rotation = angle;


        // Flashlight Switch
        if (Time.timeScale > 0 && (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Space))) // Right click or Space to toggle flashlight
        {
            flashlightOn = !flashlightOn;
            if (flashlight != null)
            {
                flashlight.SetActive(flashlightOn);
            }
        }

        // interaction click, left click
        if (Input.GetMouseButtonDown(0))
        {
            // Convert the mouse position from screen coordinates to world coordinates
            Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Send out a ray, the direction is Vector2.zero, means only the position of this point is detected
            RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero);

            if (hit.collider != null)
            {
                //check distance
                float distance = Vector2.Distance(transform.position, hit.point);

                if (distance <= maxInteractDistance)
                {
                    // try to get InteractiveObject
                    CommonInteractive interactive = hit.collider.GetComponent<CommonInteractive>();
                    if (interactive != null)
                    {
                        interactive.Interact();
                    }
                }
            }
        }

        // Battery management
        if (flashlightOn)
        {
            currentBatteryLife -= batteryDrainRate * Time.deltaTime;
            currentBatteryLife = Mathf.Clamp(currentBatteryLife, 0f, batteryLife);
            if (currentBatteryLife <= 0f)
            {
                flashlightOn = false;
                if (flashlight != null)
                {
                    flashlight.SetActive(flashlightOn);
                }
                Debug.Log("Flashlight Battery Died");
            }
        }
        else
        {

            if (currentBatteryLife < batteryLife)
            {
                currentBatteryLife += batteryRechargeRate * Time.deltaTime;
                currentBatteryLife = Mathf.Clamp(currentBatteryLife, 0f, batteryLife);
            }
        }

        // Update battery display
        SetBatteryHealth(currentBatteryLife, batteryLife);
    }

    void FixedUpdate()
    {
        // Movement
        rb.linearVelocity = moveInput * moveSpeed;
    }

    void SetBatteryHealth(float current, float max)
    {
        float percent = Mathf.Clamp01(current / max);
        if (batteryText != null)
        {
            batteryText.text = percent.ToString("P0"); // Display current and max battery life
        }
        fillBar.sizeDelta = new Vector2(maxWidth * percent, fillBar.sizeDelta.y);
        fillImage.color = Color.Lerp(Color.red, Color.green, percent);
    }
}
