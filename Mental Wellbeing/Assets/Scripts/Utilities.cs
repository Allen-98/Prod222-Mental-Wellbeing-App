using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Utilities : MonoBehaviour
{
    // Script for all sorts of random stuff lol

    public void Test()
    {
        Debug.Log("Test Success!");
    }

    public void GotoScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        Time.timeScale = 1f;
    }


    // Save testing

    public void DeleteSave()
    {
        GameSave.Reset();
    }

    public void PrintSave()
    {
        string result = "";
        result += "=+= Game Save =+=\n";
        result += "Journal Entries [" + GameSave.saveData.journalEntries.Count + "]:\n";
        foreach(JournalEntry entry in GameSave.saveData.journalEntries)
        {
            result += "    " + entry.emoticon.ToString() + " | '" + entry.text + "' | " + entry.dateTime.ToString() + "\n";
        }
        result += "Level: " + GameSave.saveData.level + "\n";
        result += "XP: " + GameSave.saveData.xp + "/" + GameSave.saveData.xpToNextLevel + "\n";
        result += "-----";
        Debug.Log(result);
    }


}
