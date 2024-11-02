using UnityEngine;

public class TouchMovement : MonoBehaviour
{
    public float rotationFactor = 0.1f;    // Factor to control how much the swipe affects the rotation speed
    private Vector2 startPos;              // Starting position of the touch
    private bool isSwipe;                  // Boolean to check if it's a swipe gesture
    public float swipeThreshold = 50f;     // Minimum distance for swipe detection
    public float moveSpeed = 5f;           // Movement speed for swiping
    public float rotationSpeed = 200f;     // Speed of smooth rotation for turning
    private float targetAngle;             // Target angle for smooth rotation
    private bool isRotating = false;       // Flag to check if rotating is in progress

    private Camera mainCamera;             // Camera reference for raycasting

    void Start()
    {
        mainCamera = Camera.main;  // Get the main camera
    }

    void Update()
    {
        // Handle touch input
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Detect single-finger or two-finger gestures
            if (Input.touchCount == 1)
            {
                // Handle single-finger swipe for rotation or movement
                if (touch.phase == TouchPhase.Began)
                {
                    startPos = touch.position;  // Store the initial touch position
                    isSwipe = false;            // Reset swipe flag
                }

                if (touch.phase == TouchPhase.Moved)
                {
                    Vector2 touchDelta = touch.position - startPos;  // Calculate movement

                    // Check if swipe distance is greater than the threshold
                    if (touchDelta.magnitude > swipeThreshold)
                    {
                        isSwipe = true;  // Set swipe flag
                        HandleSingleFingerSwipe(touchDelta);  // Call function to handle single-finger swipe
                    }
                }
            }
            else if (Input.touchCount == 2)
            {
                // Handle two-finger swipe for moving left or right
                Touch touch1 = Input.GetTouch(1);
                if (touch.phase == TouchPhase.Moved || touch1.phase == TouchPhase.Moved)
                {
                    Vector2 touchDelta1 = touch1.position - startPos;
                    if (touchDelta1.magnitude > swipeThreshold)
                    {
                        HandleTwoFingerSwipe(touchDelta1);  // Call function to handle two-finger swipe
                    }
                }
            }
        }
    }

    // Handle single-finger swipe for rotation or forward/backward movement
    void HandleSingleFingerSwipe(Vector2 direction)
    {
        // Normalize swipe direction
        direction.Normalize();

        // Move forward/backward or rotate left/right based on swipe direction
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            RotatePlayerNaturally(direction.x);  // Single-finger horizontal swipe rotates the player
        }
        else
        {
            if (direction.y > 0)
            {
                MoveForward();
            }
            else
            {
                MoveBackward();
            }
        }
    }

    // Handle two-finger swipe for horizontal movement only (no rotation)
    void HandleTwoFingerSwipe(Vector2 direction)
    {
        // Normalize swipe direction
        direction.Normalize();

        // Move left or right based on swipe direction (horizontal movement only)
        if (direction.x > 0)
        {
            MoveRight();
        }
        else if (direction.x < 0)
        {
            MoveLeft();
        }
    }

    // Rotate the player naturally based on swipe length and direction
    void RotatePlayerNaturally(float swipeDirection)
    {
        // Apply a smooth rotation based on swipe direction (positive for right, negative for left)
        float rotationAmount = swipeDirection * rotationFactor; // The swipeDirection controls left/right, rotationFactor controls speed
        transform.Rotate(Vector3.up, rotationAmount * Time.deltaTime * 100f);  // Rotate around the Y axis for horizontal rotation
    }

    // Move functions for swipe gestures
    void MoveRight()
    {
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
    }

    void MoveLeft()
    {
        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
    }

    void MoveForward()
    {
        // Keep the Y position unchanged, only move along the XZ plane
        Vector3 forward = transform.forward;
        forward.y = 0; // Prevent downward movement by eliminating the Y component
        forward.Normalize();
        transform.position += forward * moveSpeed * Time.deltaTime;
    }

    void MoveBackward()
    {
        // Keep the Y position unchanged, only move along the XZ plane
        Vector3 backward = -transform.forward;
        backward.y = 0; // Prevent downward movement by eliminating the Y component
        backward.Normalize();
        transform.position += backward * moveSpeed * Time.deltaTime;
    }
}
