using UnityEngine;

public class CameraClass : MonoBehaviour
{
    public Transform player;  // The player to follow
    public float smoothSpeed = 5f;  // Speed of camera movement

    public Vector2 minBounds;  // Manually set min bounds (bottom-left corner)
    public Vector2 maxBounds;  // Manually set max bounds (top-right corner)

    private float camHalfWidth;
    private float camHalfHeight;

    void Start()
    {
        if (player == null)
        {
            Debug.LogError("Player Transform is not assigned in CameraFollow2D!");
            return;
        }

        // Get camera half size (to prevent unwanted zooming)
        Camera cam = Camera.main;
        camHalfHeight = cam.orthographicSize;
        camHalfWidth = camHalfHeight * cam.aspect;
    }

    void LateUpdate()
    {
        if (player == null) return;

        // Follow the player
        Vector3 targetPos = new Vector3(player.position.x, player.position.y, transform.position.z);

        // Clamp position to keep camera inside bounds
        float clampedX = Mathf.Clamp(targetPos.x, minBounds.x + camHalfWidth, maxBounds.x - camHalfWidth);
        float clampedY = Mathf.Clamp(targetPos.y, minBounds.y + camHalfHeight, maxBounds.y - camHalfHeight);

        // Smoothly move camera
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, new Vector3(clampedX, clampedY, transform.position.z), smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
    }
}
