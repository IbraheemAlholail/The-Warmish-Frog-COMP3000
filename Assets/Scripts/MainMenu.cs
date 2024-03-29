using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    public Scene scene;
    public GameObject mainMenu;
    public GameObject optionsMenu;
    void Start()
    {

    }

    void Update()
    {
        
    }
    public void loadGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Overworld");
    }
    public void quitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
    public void loadOptions()
    {
        SceneManager.LoadScene("OptionsMenu");
    }
    public void optionsSwitch()
    {
        if (mainMenu.activeSelf)
        {
            mainMenu.SetActive(false);
            optionsMenu.SetActive(true);
        }
        else
        {
            mainMenu.SetActive(true);
            optionsMenu.SetActive(false);
        }
    }
    
}
