using UnityEngine;

public class PinchToZoom : MonoBehaviour
{
    public Camera sceneCamera;            // Reference to the camera
    public float zoomSpeed = 0.1f;        // Speed of zooming in/out
    public float minFOV = 30f;            // Minimum FOV (zoom in)
    public float maxFOV = 90f;            // Maximum FOV (zoom out)

    private float touchDeltaMagnitude;    // Magnitude of touch movement
    private float previousTouchDeltaMag;  // Previous frame's touch delta magnitude

    void Start()
    {
        // If no camera is assigned, use the main camera
        if (sceneCamera == null)
        {
            sceneCamera = Camera.main;
        }
    }

    void Update()
    {
        // Check for pinch zoom
        if (Input.touchCount == 2)
        {
            // Store both touches
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            // Find the position in the previous frame of each touch
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            // Calculate the difference in the distance between touches in this frame and the previous frame
            previousTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            touchDeltaMagnitude = (touchZero.position - touchOne.position).magnitude;

            // Calculate the difference in magnitudes between the previous and current frames
            float deltaMagnitudeDiff = previousTouchDeltaMag - touchDeltaMagnitude;

            // Adjust the Field of View (FOV) of the camera to zoom in or out
            sceneCamera.fieldOfView += deltaMagnitudeDiff * zoomSpeed;

            // Clamp the FOV to ensure it stays within the defined limits
            sceneCamera.fieldOfView = Mathf.Clamp(sceneCamera.fieldOfView, minFOV, maxFOV);
        }
    }
}
