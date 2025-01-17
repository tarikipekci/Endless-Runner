using UnityEngine;

public class SwipeManager : MonoBehaviour
{
    public static bool tap, swipeLeft, swipeRight, swipeUp, swipeDown;
    private bool isDragging;
    private Vector2 startTouch, swipeDelta;

    private void Update()
    {
        if (!Events.isGamePaused)
        {
            tap = swipeDown = swipeUp = swipeLeft = swipeRight = false;

            #region Standalone Inputs

            if (Input.GetMouseButtonDown(0))
            {
                tap = true;
                isDragging = true;
                startTouch = Input.mousePosition;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                isDragging = false;
                Reset();
            }

            #endregion

            #region Mobile Input

            if (Input.touches.Length > 0)
            {
                if (Input.touches[0].phase == TouchPhase.Began)
                {
                    tap = true;
                    isDragging = true;
                    startTouch = Input.touches[0].position;
                }
                else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
                {
                    isDragging = false;
                    Reset();
                }
            }

            #endregion

            //Calculate the distance
            swipeDelta = Vector2.zero;
            if (isDragging)
            {
                if (Input.touches.Length < 0)
                    swipeDelta = Input.touches[0].position - startTouch;
                else if (Input.GetMouseButton(0))
                    swipeDelta = (Vector2)Input.mousePosition - startTouch;
            }

            //Did we cross the distance?
            if (swipeDelta.magnitude > 100)
            {
                //Which direction?
                float x = swipeDelta.x;
                float y = swipeDelta.y;
                if (Mathf.Abs(x) > Mathf.Abs(y))
                {
                    //Left or Right
                    if (x < 0)
                        swipeLeft = true;
                    else
                        swipeRight = true;
                }
                else
                {
                    //Up or Down
                    if (y < 0)
                        swipeDown = true;
                    else
                        swipeUp = true;
                }

                Reset();
            }
        }
    }

    private void Reset()
    {
        startTouch = swipeDelta = Vector2.zero;
        isDragging = false;
    }
}