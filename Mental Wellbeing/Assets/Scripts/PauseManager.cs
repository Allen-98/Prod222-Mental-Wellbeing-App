using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseManagerUI;

    public Image pauseButton;
    public Sprite iconPause;
    public Sprite iconResume;
    public Sprite iconHome;

    private bool isHome = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void Resume()
    {
        pauseManagerUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        pauseButton.sprite = iconPause;
    }

    public void Pause()
    {
        if (!isHome)
        {
            pauseManagerUI.SetActive(true);
            Time.timeScale = 0f;
            GameIsPaused = true;
            pauseButton.sprite = iconResume;
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }

    public void SetHomeOn()
    {
        isHome = true;
        pauseButton.sprite = iconHome;
    }
    public void SetHomeOff()
    {
        isHome = false;
        if (GameIsPaused)
        {
            pauseButton.sprite = iconResume;
        }
        else
        {
            pauseButton.sprite = iconPause;
        }
    }

    public void TogglePause()
    {
        if (GameIsPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    public void LoadScene()
    {
        Resume();
       
        SceneManager.LoadScene(0);
    }

    public static bool IsPaused()
    {
        return GameIsPaused;
    }

    
}

