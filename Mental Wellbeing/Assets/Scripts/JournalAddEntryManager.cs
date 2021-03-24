using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JournalAddEntryManager : MonoBehaviour
{

    [Header("References")]
    [Tooltip("Button used to close/save the journal entry.")]
    public Button continueButton;
    [Tooltip("Input field for user to add text to entry.")]
    public InputField inputField;
    [Tooltip("XP bar popup.")]
    public GameObject xpBarPopup;

    [Header("Settings")]
    [Tooltip("How much XP to give the player on journal entry completion.")]
    public int xpRewardAmount = 50;

    [Header("Public Variables")]
    [Tooltip("The current emoticon selected.")]
    public Emoticon selectedEmoticon = Emoticon.NONE;


    void Start()
    {
        continueButton.interactable = false;
    }

    void Update()
    {
        
    }

    public void EmoticonClicked(Emoticon emoticon)
    {
        selectedEmoticon = emoticon;
        continueButton.interactable = emoticon != Emoticon.NONE;
    }

    public void ContinueClicked()
    {
        bool firstEntry = GameSave.CheckNoEntryToday();

        GameSave.AddJournalEntry(new JournalEntry(selectedEmoticon, inputField.text));

        if (firstEntry)
        {
            GameSave.AddXP(xpRewardAmount);
            if (xpBarPopup != null) Instantiate(xpBarPopup);
        }

        GameSave.Save();
        

        GameObject displayScreen = GameObject.Find("JournalDisplayScreen");
        if (displayScreen != null)
        {
            JournalDisplayScreen display = displayScreen.GetComponent<JournalDisplayScreen>();
            if (display != null) display.FillEntries();
        }

        Destroy(gameObject);
    }

    public void Cancel()
    {
        Destroy(gameObject);
    }


 }
