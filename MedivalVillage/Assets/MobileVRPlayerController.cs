using UnityEngine;

public class MobileVRPlayerController : MonoBehaviour
{
    public float speed = 5f;                // Speed of player movement
    public Transform playerCamera;          // Reference to the player's camera
    public float rotationSpeed = 2f;        // Speed of rotation

    void Start()
    {
        // Enable gyroscope if supported
        if (SystemInfo.supportsGyroscope)
        {
            Input.gyro.enabled = true;
        }
    }

    void Update()
    {
        // Handle movement based on touch input
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Move the player based on touch movement
            if (touch.phase == TouchPhase.Moved)
            {
                Vector3 moveDirection = new Vector3(touch.deltaPosition.x, 0, touch.deltaPosition.y);
                moveDirection = playerCamera.TransformDirection(moveDirection);
                moveDirection.y = 0; // Prevent vertical movement
                transform.position += moveDirection * speed * Time.deltaTime;
            }

            // Handle rotation based on touch input
            if (touch.phase == TouchPhase.Moved)
            {
                float rotX = touch.deltaPosition.x * rotationSpeed;
                float rotY = touch.deltaPosition.y * rotationSpeed;

                // Rotate the player camera based on touch input
                playerCamera.Rotate(-rotY, rotX, 0);
            }
        }

        // Handle head movement using gyroscope
        if (SystemInfo.supportsGyroscope)
        {
            Vector3 gyroRotation = Input.gyro.rotationRateUnbiased;
            playerCamera.Rotate(gyroRotation.x, gyroRotation.y, 0);
        }
    }
}
