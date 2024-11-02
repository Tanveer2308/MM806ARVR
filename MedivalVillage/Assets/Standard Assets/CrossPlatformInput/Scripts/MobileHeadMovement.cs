using UnityEngine;

public class MobileHeadMovement : MonoBehaviour
{
    public float speed = 5.0f;  // Speed of movement
    public Transform playerCamera;  // Assign the Camera or player’s view

    void Update()
    {
        // Get the phone’s rotation from the gyroscope
        Vector3 forwardDirection = playerCamera.forward;
        forwardDirection.y = 0;  // Ignore vertical movement to keep the player on the ground

        // Move the player in the direction the phone is facing
        transform.Translate(forwardDirection * speed * Time.deltaTime);
    }
}
