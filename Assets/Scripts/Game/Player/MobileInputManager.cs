//Mobile Input Manager
//by Jackson
//Last edited 21/9/2023 4:39 PM

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class MobileInputManager : MonoBehaviour
{
    Vector2 m_fingerPosition;
    Vector2 m_initialFingerPos;
    [SerializeField]
    [Tooltip("The distance from the initial finger placement at which the player begins drifting.\nIgnores the X axis.")]
    float m_driftThreashhold;
    bool m_userDrifting;
    bool m_readInputs = true;
    [SerializeField] bool m_allowTurnActionsWhileDrifting = false;

    PlayerMovement m_movement;

    [SerializeField]
    Image m_leftIcon;
    [SerializeField]
    Image m_rightIcon;
    [SerializeField]
    Image m_initalTouchIndecator;
    public float DEBUG;

    [SerializeField]
    ClubSwing m_clubSwing;

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
                if (m_clubSwing.m_waitingForPlayer)
                {
                    m_clubSwing.TriggerStartSwing();
                }
                else
                {
                    //Save positions
                    m_fingerPosition = Input.GetTouch(0).position;
                    if (Input.GetTouch(0).phase == TouchPhase.Began)
                    {
                        m_initialFingerPos = m_fingerPosition;
                        m_initalTouchIndecator.rectTransform.anchoredPosition = new Vector2(m_initialFingerPos.x / 10, 0);
                    }
                    else if(Input.GetTouch(0).phase == TouchPhase.Ended)
                    {
                        //m_initalTouchIndecator.rectTransform.anchoredPosition = new Vector2(m_initalTouchIndecator.rectTransform.sizeDelta.x * -2, 0);
                    }

                    //Compare positions
                    if (m_fingerPosition.x > m_initialFingerPos.x)
                    {
                        if (!m_userDrifting) RightSideAction(); 
                        else if (m_allowTurnActionsWhileDrifting) RightSideAction();
                    }
                    else if (m_fingerPosition.x < m_initialFingerPos.x)
                    {
                        if (!m_userDrifting) LeftSideAction();
                        else if (m_allowTurnActionsWhileDrifting) LeftSideAction();
                    }

                    //Check the difference between the initial finger placement and current placement
                    //Not using |ABS| here as we only want to check if the player is pulling down
                    if (!m_userDrifting && m_initialFingerPos.y - m_fingerPosition.y > m_driftThreashhold)
                    {
                        StartDrifting();
                    }
                    if (m_userDrifting && Input.GetTouch(0).phase == TouchPhase.Ended)
                    {
                        EndDrifting();
                    }
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
            m_leftActionStart.Invoke();

        if (Input.GetTouch(0).phase == TouchPhase.Ended)
        {
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
            m_rightActionStart.Invoke();

        if (Input.GetTouch(0).phase == TouchPhase.Ended)
        {
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
