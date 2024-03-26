using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    public Scene scene;
    // play music while in main menu, but not in game
    //public AudioSource mainMenuMusic;


    void Start()
    {
    }

    void Update()
    {
        
    }
    public void loadGame()
    {
        Time.timeScale = 1f;
        //mainMenuMusic.Stop();
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
    
}
