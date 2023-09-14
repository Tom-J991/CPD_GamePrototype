//Mobile Input Manager
//by Jackson
//Last edited 14/9/2023 11:13 am

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class MobileInputManager : MonoBehaviour
{
    Vector2 m_fingerPosition;
    float m_initialFingerY;
    [SerializeField]
    [Tooltip("The distance from the initial finger placement at which the player begins drifting.\nIgnores the X axis.")]
    float m_driftThreashhold;
    bool m_userDrifting;
    bool m_readInputs = true;

    PlayerMovement m_movement;

    [SerializeField]
    Image m_leftIcon;
    [SerializeField]
    Image m_rightIcon;

    [SerializeField] UnityEvent m_leftActionStart = new UnityEvent();
    [SerializeField] UnityEvent m_rightActionStart = new UnityEvent();
    //[SerializeField] UnityEvent m_leftDriftActionStart = new UnityEvent();
    //[SerializeField] UnityEvent m_rightDriftActionStart = new UnityEvent();
    [SerializeField] UnityEvent m_leftActionEnd = new UnityEvent();
    [SerializeField] UnityEvent m_rightActionEnd = new UnityEvent();
    //[SerializeField] UnityEvent m_leftDriftActionEnd = new UnityEvent();
    //[SerializeField] UnityEvent m_rightDriftActionEnd = new UnityEvent();
    [SerializeField] UnityEvent m_driftActionStart = new UnityEvent();
    [SerializeField] UnityEvent m_driftActionEnd = new UnityEvent();

    private void Start()
    {
        m_movement = FindObjectOfType<PlayerMovement>();
    }
    void Update()
    {
        if (m_readInputs)
        {
            //First check if there is any inputs
            //I think that this will only check the first input so if wanna change it, it may need some work -J
            if (Input.touchCount > 0)
            {
                //Save positions
                m_fingerPosition = Input.GetTouch(0).position;
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    m_initialFingerY = m_fingerPosition.y;
                }

                //Compare positions
                //If we're over half of the screen's width, we're on the right side
                if (m_fingerPosition.x > Screen.width / 2)
                {
                    RightSideAction();
                }
                else
                {
                    LeftSideAction();
                }

                //Check the difference between the initial finger placement and current placement
                //Not using |ABS| here as we only want to check if the player is pulling down
                if (m_initialFingerY - m_fingerPosition.y > m_driftThreashhold)
                {
                    StartDrifting();
                }
                else if (m_userDrifting)
                {
                    EndDrifting();
                }
            }
        }
    }
    private void LeftSideAction()
    {
        Debug.Log("Left side pressed");
        /*
        if (m_userDrifting)
        {
            m_leftIcon.color = Color.blue;
            //Active drift function
            m_leftDriftActionStart.Invoke();
        }
        else
        {
        }
         */
            m_leftIcon.color = Color.green;
            m_leftActionStart.Invoke();

        if (Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            m_leftIcon.color = Color.white;
            m_leftActionEnd.Invoke();
        }
    }
    private void RightSideAction()
    {
        Debug.Log("Right side pressed");
        /*
        if(m_userDrifting)
        {
            m_rightIcon.color = Color.blue;
            m_rightDriftActionStart.Invoke();
        }
        else
        {
        }
         */
            m_rightIcon.color = Color.green;
            m_rightActionStart.Invoke();

        if (Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            m_rightIcon.color = Color.white;
            m_rightActionEnd.Invoke();
        }
    }
    private void StartDrifting()
    {
        Debug.Log("Player is now drifting");
        m_userDrifting = true;
        m_driftActionStart.Invoke();
    }
    private void EndDrifting()
    {
        m_userDrifting = false;
        m_driftActionEnd.Invoke();
    }
    public void ToggleInputReading()
    {
        m_readInputs = !m_readInputs;
    }
    public void SetInputReading(bool setting)
    {
        m_readInputs = setting;
    }
}
