using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject GUIPanel;
    public GameObject deathPanel;
    public GameObject winPanel;
    public GameObject deathMusic;



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
                fixSlider();
            }
        }
  
    }

    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        GUIPanel.SetActive(true);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        GUIPanel.SetActive(false);
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
        Destroy(GameObject.Find("Music"));
        SceneManager.LoadScene("MainMenu");
    }

    public void RestartGame()
    {

        SceneManager.LoadScene("Overworld");
        ResumeGame();
    }

    public void onDeath()
    {
        Time.timeScale = 0f;
        deathPanel.SetActive(true);
        GUIPanel.SetActive(false);
        GameObject.Find("Music").GetComponent<Music>().musicSource.Stop();
        deathMusic.SetActive(true);
        deathMusic.GetComponent<AudioSource>().Play();
        deathMusic.GetComponent<AudioSource>().volume = GameObject.Find("Music").GetComponent<Music>().musicSource.volume;
    }

    public void fixSlider()
    {
        GameObject.Find("Volume Slider").GetComponent<UnityEngine.UI.Slider>().value = GameObject.Find("Music").GetComponent<Music>().musicSource.volume;
    }

    public void onWin()
    {
        Time.timeScale = 0f;
        winPanel.SetActive(true);
        GUIPanel.SetActive(false);
    }
    
}


