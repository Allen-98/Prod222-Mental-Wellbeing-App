using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class HomeManager : MonoBehaviour
{
    //
    // Various methods for managing the home screen.
    //

    [Header("References")]
    [Tooltip("Prefab to instantiate when game is started.")]
    public GameObject addJournalEntryPrefab;
    [Tooltip("Prefab to instantiate when game is played for the first time.")]
    public GameObject firstTimePrefab;
    [Tooltip("Object for the settings panel.")]
    public GameObject settingsPanel;
    [Tooltip("Object for the delete save confirmation popup.")]
    public GameObject deleteSavePanel;
    [Tooltip("Slider to control the volume.")]
    public Slider volumeSlider;

    [Header("Progression")]
    public GameObject book1;
    public GameObject book2;
    public GameObject book3;
    public GameObject plant1;
    public GameObject plant2;
    public GameObject plant3;
    public GameObject plant4;
    public GameObject plant5;
    public GameObject plant6;
    public static DateTime lastEnterTime;
    public static DateTime todayTime;
    public static Boolean isNewday;


    private int currentLevel = -1;

    void Start()
    {
        CheckTime();

        if (GameSave.isFirstStarted)
        {
            Instantiate(firstTimePrefab);
            GameSave.isFirstStarted = false;
            GameSave.isStarted = false;
            lastEnterTime = DateTime.Now;
            todayTime = DateTime.Today;
            isNewday = true;
        }
        else if (GameSave.isStarted)
        {
            if (GameSave.CheckNoEntryToday()) Instantiate(addJournalEntryPrefab);
            GameSave.isStarted = false;
            
        }

        volumeSlider.value = AudioListener.volume;
    }

    void Update()
    {
        if (currentLevel != GameSave.saveData.level)
        {
            currentLevel = GameSave.saveData.level;
            UpdateProgression();
        }
    }

    public void ToggleSettings()
    {
        settingsPanel.SetActive(!settingsPanel.activeSelf);
    }

    public void ToggleDeleteSave()
    {
        deleteSavePanel.SetActive(!deleteSavePanel.activeSelf);
    }

    public void OnVolumeChanged()
    {
        AudioListener.volume = volumeSlider.value;
    }

    private void UpdateProgression()
    {
        book1.SetActive(currentLevel >= 2);
        plant1.SetActive(currentLevel == 3 || currentLevel == 4);
        book2.SetActive(currentLevel >= 4);
        plant2.SetActive(currentLevel == 5 || currentLevel == 6);
        book3.SetActive(currentLevel >= 6);
        plant3.SetActive(currentLevel == 7);
        plant4.SetActive(currentLevel == 8);
        plant5.SetActive(currentLevel == 9);
        plant6.SetActive(currentLevel >= 10);
    }

    public static void CheckTime()
    {
        DateTime timeNow = DateTime.Now;
        DateTime timeToday = DateTime.Today;
        
        TimeSpan span1 = lastEnterTime - todayTime;
        
        TimeSpan zeroTime = new TimeSpan(0, 0, 0, 0);

        TimeSpan span3 = timeNow - todayTime;

        TimeSpan dayLimit = new TimeSpan(24, 0, 0, 0, 0);

        if (span3 >= zeroTime && span3 < dayLimit)
        {
            if (span1 < zeroTime)
            {
                isNewday = true;
                lastEnterTime = timeNow;
                todayTime = timeToday;
            } 
            else if (span1 >= zeroTime)
            {
                isNewday = false;
                lastEnterTime = timeNow;

            }
        }
        else if (span3 < zeroTime)
        {
            isNewday = false;
            lastEnterTime = timeNow;
        }

        
    }
}
