using UnityEngine;

public class MobileTouchMovement : MonoBehaviour
{
    public float swipeSpeed = 2.0f;  // Speed for swipe movement
    public float rotateSpeed = 50f;  // Speed for rotating the view with touch

    void Update()
    {
        // Detect swipe
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                // Start of touch
            }

            else if (touch.phase == TouchPhase.Moved)
            {
                Vector2 touchDelta = touch.deltaPosition;

                // Swipe up or down for forward/backward movement
                if (Mathf.Abs(touchDelta.y) > Mathf.Abs(touchDelta.x))
                {
                    float move = touchDelta.y * swipeSpeed * Time.deltaTime;
                    transform.Translate(0, 0, move);
                }
                // Swipe left or right for rotation
                else if (Mathf.Abs(touchDelta.x) > Mathf.Abs(touchDelta.y))
                {
                    float rotate = touchDelta.x * rotateSpeed * Time.deltaTime;
                    transform.Rotate(0, rotate, 0);
                }
            }
        }
    }
}
