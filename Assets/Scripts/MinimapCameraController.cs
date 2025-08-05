using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Camera))]
public class MinimapCameraController : MonoBehaviour
{
    [Header("Tilemap and Grid")]
    public Tilemap groundTilemapBedroom;
    public Tilemap groundTilemapKitchen;
    public Tilemap groundTutorial;
    public Grid grid;

    [Header("Minimap Camera Settings")]
    public float cameraHeight = 10f;
    public float padding = 1f;

    private Camera minimapCamera;

    void Start()
    {
        minimapCamera = GetComponent<Camera>();

        if (groundTilemapBedroom == null || groundTilemapKitchen == null || grid == null)
        {
            Debug.LogError("Ground tilemap or Grid not assigned!");
            return;
        }
    }

    public void PositionAndSizeCamera(int level)
    {
        Tilemap groundTilemap;
        switch (level)
        {
            case 0:
                groundTilemap = groundTutorial;
                break;
            case 1:
                groundTilemap = groundTilemapBedroom;
                break;
            case 2:
                groundTilemap = groundTilemapKitchen;
                break;
            default:
                Debug.LogWarning("Unexpected level index: " + level + ". Defaulting to Bedroom tilemap.");
                groundTilemap = groundTilemapBedroom;
                break;
        }

        BoundsInt bounds = groundTilemap.cellBounds;

        Vector3Int bottomLeft = new Vector3Int(bounds.xMin, bounds.yMin - 1, 0);
        Vector3Int topRight = new Vector3Int(bounds.xMax - 1, bounds.yMax, 0);

        Vector3 worldBottomLeft = grid.GetCellCenterWorld(bottomLeft);
        Vector3 worldTopRight = grid.GetCellCenterWorld(topRight);

        // Compute center of tilemap
        Vector3 center = (worldBottomLeft + worldTopRight) / 2f;

        // Set camera position (top-down view)
        transform.position = new Vector3(center.x, center.y, -cameraHeight);
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);

        // Adjust orthographic size
        float width = Mathf.Abs(worldTopRight.x - worldBottomLeft.x) + padding;
        float height = Mathf.Abs(worldTopRight.y - worldBottomLeft.y) + padding;

        minimapCamera.orthographic = true;
        minimapCamera.orthographicSize = Mathf.Max(width / minimapCamera.aspect, height) / 2f;
    }
}