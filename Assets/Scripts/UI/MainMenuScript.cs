//Main Menu Script
//by: Jackson
//Last Edited: 6/9/2023 4:35 pm

using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField]
    [Tooltip("A list of all the level that the player can load.")]
    string[] m_levelNames;
    [SerializeField]
    [Tooltip("The dropdown that the player interacts with.")]
    TMP_Dropdown m_Dropdown;
    // Start is called before the first frame update
    void Start()
    {
        //Load in all the names of the scenes to load
        foreach(var levelName in m_levelNames)
        {
            //Create a new data value for the dropdown
            TMP_Dropdown.OptionData data = new TMP_Dropdown.OptionData();
            data.text = levelName;
            //Add the data to the dropdown menu
            m_Dropdown.options.Add(data);
        }
        m_Dropdown.GetComponentInChildren<TextMeshProUGUI>().text = m_Dropdown.options[0].text;
    }

    /// <summary>
    /// Attempts to load the scene that is currently selected in the dropdown menu
    /// Uses the text loaded in the input field.
    /// This saves can having to organise levels in build settings but does mean that the names in the input field need to match the scene name
    /// </summary>
    public void LoadScene()
    {
        Debug.Log("Attempting to load \"" + m_Dropdown.options[m_Dropdown.value].text + "\"");

        try
        {
            SceneManager.LoadScene(m_Dropdown.options[m_Dropdown.value].text);
        }
        catch (Exception e)
        {
            Debug.Log("Failed - Exception: " + e);

        }
    }
}
