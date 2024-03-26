using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject otherCanvas;

    void Update()
    {
        if (Input.GetButtonDown("pause"))
        {
            if (GameIsPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
  
    }

    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        otherCanvas.SetActive(true);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        otherCanvas.SetActive(false);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
