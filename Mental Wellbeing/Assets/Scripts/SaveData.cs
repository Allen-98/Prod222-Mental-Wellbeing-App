using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{

    public List<JournalEntry> journalEntries;

    public int level = 1;
    public int xp = 0;
    public int xpToNextLevel = 100;

    public SaveData()
    {
        journalEntries = new List<JournalEntry>();
    }

}
