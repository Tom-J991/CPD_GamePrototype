//Menu Manager script
//by Jackson
//Last Edited 20/9/2023 11:56 AM

using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Tooltip("The menus to display game object.")]
    public GameObject[] m_menus;
    int m_currentIndex = 0;
    [Tooltip("Enables or disables the pause function.")]
    public bool m_doPause = true;
    [Tooltip("The keyboard input to pause the game - ignore for mobile.")]
    public KeyCode m_pauseMenuKey;

    GameManager gm;
    void Start()
    {
        gm = FindObjectOfType<GameManager>();    
    }
    /// <summary>
    /// Allows for a quick switch between 2 menus
    /// </summary>
    /// <param name="hide"></param>
    /// <param name="display"></param>
    public void SwitchMenu(int hide, int display)
    {
        m_menus[hide].SetActive(false);
        m_menus[display].SetActive(true);
    }
    /// <summary>
    /// Enables the menu on the unique key being presses
    /// </summary>
    private void Update()
    {
        if (Input.GetKeyDown(m_pauseMenuKey) && m_doPause)
        {
            PauseGame();
        }
    }
    public int GetIndexOfMenu(GameObject menu)
    {
        for(int i = 0; i < m_menus.Length; i++)
        {
            if (m_menus[i] == menu) return i;
        }
        return -1;
    }
    /// <summary>
    /// Displays the desiered menu and updates the current menu index
    /// </summary>
    /// <param name="i"></param>
    public void DisplayMenu(int i)
    {
        m_menus[i].SetActive(true);
        m_currentIndex = i;
    }
    /// <summary>
    /// Hides all other menus then displays the wanted menu
    /// </summary>
    /// <param name="i"></param>
    public void HardDisplayMenu(int i)
    {
        foreach(GameObject menu in m_menus)
        {
            if (menu.activeSelf)
                menu.SetActive(false);
        }
        DisplayMenu(i);
    }
    public void SwitchToMenu(int i)
    {
        HideCurrentMenu();
        DisplayMenu(i);
    }
    public void HardSwitchToMenu(int i)
    {
        HideCurrentMenu();
        HardDisplayMenu(i);
    }
    /// <summary>
    /// Hides the desiered menu
    /// </summary>
    /// <param name="i"></param>
    public void HideMenu(int i)
    {
        m_menus[i].SetActive(false);

        bool check = false;
        foreach(GameObject menu in m_menus)
        {
            if(menu.activeSelf == true)
            {
                check = true;
                break;
            }
            if(!check)
            {
                ResumeTime();
            }
        }
    }
    /// <summary>
    /// Hides the currently active menu
    /// </summary>
    public void HideCurrentMenu()
    {
        m_menus[m_currentIndex].SetActive(false);
    }

    public void PauseGame()
    {
        PauseTime();
        //Assumes that the pause menu is the first menu
        HardDisplayMenu(0);
        gm.UnlockMouse();
    }
    /// <summary>
    /// You won't believe what these functions do!!
    /// </summary>
    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }
    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }
    /// <summary>
    /// Loads the main menu scene
    /// </summary>
    public void QuitToMainMenu()
    {
        //TODO: replace this with the real main menu
        SceneManager.LoadScene("MainMenuScene");
    }
    /// <summary>
    /// Either quits the application in build mode or exits the editor
    /// </summary>
    public void QuitApplication()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    public void SetTimeScale(float scale)
    {
        Time.timeScale = scale;
    }
    public void PauseTime()
    {
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;
    }
    public void ResumeTime()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
    }
    /*
    public void DisplayPauseMenu()
    {
        m_menus[0].SetActive(true);
    }
    public void HidePauseMenu()
    {
        //m_pauseMenu.SetActive(false);
    }
    public void DisplayOptionsMenu()
    {
        //m_pauseMenu.SetActive(false);
        //m_optionsMenu.SetActive(true);
    }
    public void DisplayQuitMenu()
    {
        m_pauseMenu.SetActive(false);
        //m_quitMenu.SetActive(true);
    }
     */
}
