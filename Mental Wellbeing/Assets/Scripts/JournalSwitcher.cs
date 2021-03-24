using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JournalSwitcher : MonoBehaviour
{

    [Header("References")]
    [Tooltip("Object to enable/disable for the report UI screen.")]
    public GameObject reportScreen;
    [Tooltip("Object to enable/disable for the display all entries UI screen.")]
    public GameObject entriesScreen;
    [Tooltip("Prefab to instantiate when add entry button is clicked.")]
    public GameObject journalAddEntryPrefab;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void GotoReport()
    {
        entriesScreen.SetActive(false);
        reportScreen.SetActive(true);
    }

    public void GotoEntries()
    {
        entriesScreen.SetActive(true);
        reportScreen.SetActive(false);
    }

    public void GotoHome()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Home");
    }

    public void AddJournalEntry()
    {
        Instantiate(journalAddEntryPrefab);
    }
}
