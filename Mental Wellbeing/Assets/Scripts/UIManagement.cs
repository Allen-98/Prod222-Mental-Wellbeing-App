using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManagement: MonoBehaviour
{
    public GameObject winMenu;
    public GameObject loseMenu;
    public GameObject instructionAndSelfblame;
    public bool started = false;
    public GameObject xpBarPopupPrefab;

    public PauseManager pauseManager;

    public float XPperSecond = 0.5f;
    private float XPtimer;
    

    void Start() {
        Time.timeScale = 0f;
        pauseManager.SetHomeOn();
        notStarted();
        XPtimer = 1f / XPperSecond;
    }

    void Update()
    {
        if (Time.timeScale > 0f)
        {
            XPtimer -= Time.deltaTime;
            if (XPtimer <= 0f)
            {
                XPtimer = 1f / XPperSecond;
                GameSave.AddXP(1);
                //Debug.Log("Gained 1 XP");
            }
        }
    }

    public void Win()
    {
        bool levelledUp = GameSave.AddXP(15);
        winMenu.SetActive(true);
        Time.timeScale = 0f;
        if (levelledUp && xpBarPopupPrefab != null) Instantiate(xpBarPopupPrefab);
        pauseManager.SetHomeOn();
    }

    public void Lose()
    {
        //bool levelledUp = GameSave.AddXP(5);
        loseMenu.SetActive(true);
        Time.timeScale = 0f;
        //if (levelledUp && xpBarPopupPrefab != null) Instantiate(xpBarPopupPrefab);
        pauseManager.SetHomeOn();
    }

    public void Restart()
    {
        SceneManager.LoadScene("Plant");
        Time.timeScale = 1f;
    }

    public void GoBack()
    {
        //SceneManager.LoadScene("Home");
        //Time.timeScale = 1f;

    }

    public void notStarted() {
        if (started) {
            Time.timeScale = 0f;
            instructionAndSelfblame.SetActive(true);

        }
        
    }
    public void Started() {
        started = true;
        Time.timeScale = 1f;
        instructionAndSelfblame.SetActive(false);
        pauseManager.SetHomeOff();
    }

    
    //public void ParticipatingXP()
   // {
        
        //if (Time.timeScale > 0f)
        //{
            //GameSave.AddXP(5);
            //Debug.Log("helpp");
        //}
    //}
}
