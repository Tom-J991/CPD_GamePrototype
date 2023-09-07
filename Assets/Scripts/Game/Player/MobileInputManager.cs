//Mobile Input Manager
//by Jackson
//Last edited 7/9/2023 12:07 am

using UnityEngine;

public class MobileInputManager : MonoBehaviour
{
    Vector2 m_fingerPosition;
    float m_initialFingerY;
    [SerializeField]
    [Tooltip("The distance from the initial finger placement at which the player begins drifting.\nIgnores the X axis.")]
    float m_driftThreashhold;

    void Update()
    {
        //First check if there is any inputs
        //I think that this will only check the first input so if wanna change it, it may need some work -J
        if(Input.touchCount > 0)
        {
            //Save positions
            m_fingerPosition = Input.GetTouch(0).position;
            if(Input.GetTouch(0).phase == TouchPhase.Began)
            {
                m_initialFingerY = m_fingerPosition.y;
            }

            //Compare positions
            //If we're over half of the screen's width, we're on the right side
            if(m_fingerPosition.x > Screen.width / 2)
            {
                RightSideAction();
            }
            else
            {
                LeftSideAction();
            }

            //Check the difference between the initial finger placement and current placement
            //Not using |ABS| here as we only want to check if the player is pulling down
            if(m_initialFingerY - m_fingerPosition.y > m_driftThreashhold)
            {
                StartDrifting();
            }
        }
    }
    private void LeftSideAction()
    {
        Debug.Log("Left side pressed");
    }
    private void RightSideAction()
    {
        Debug.Log("Right side pressed");

    }
    private void StartDrifting()
    {
        Debug.Log("Player is now drifting");
    }
}
