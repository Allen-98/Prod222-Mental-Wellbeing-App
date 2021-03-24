using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class GameSave
{

    private const string SAVE_NAME = "save.dat";

    public static SaveData saveData = new SaveData();

    public static bool isFirstStarted = false;
    public static bool isStarted = false;

    public static int currentLevel = 1;
    public static float currentProgress = 0f;


    [RuntimeInitializeOnLoadMethod]
    private static void OnGameStart()
    {
        if (FileExists())
        {
            Load();
        }
        else
        {
            isFirstStarted = true;
        }
        isStarted = true;
    }

    public static void Save()
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream file = File.Create(GetFilePath());
        binaryFormatter.Serialize(file, saveData);
        file.Close();
        //Debug.Log("Game saved!");
    }

    public static void Load()
    {
        if (FileExists())
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream file = File.Open(GetFilePath(), FileMode.Open);
            saveData = (SaveData)binaryFormatter.Deserialize(file);
            file.Close();
            //Debug.Log("Game loaded!");

            currentLevel = saveData.level;
            if (saveData.xpToNextLevel == 0) saveData.xpToNextLevel = 100;
            currentProgress = (float)saveData.xp / saveData.xpToNextLevel;
        }
        else
        {
            Debug.LogError("No save data.");
        }
    }

    public static void Reset()
    {
        if (FileExists())
        {
            File.Delete(GetFilePath());
            //Debug.Log("Game save reset (deleted save file)!");
        }
        else
        {
            //Debug.Log("Game save reset (no file)!");
        }
        saveData = new SaveData();
    }

    public static bool FileExists()
    {
        return File.Exists(GetFilePath());
    }

    private static string GetFilePath()
    {
        return Application.persistentDataPath + "/" + SAVE_NAME;
    }

    public static void AddJournalEntry(JournalEntry entry)
    {
        saveData.journalEntries.Add(entry);
    }

    public static bool AddXP(int amount)
    {
        // Returns true if the player levels up with added XP.
        bool levelledUp = false;

        saveData.xp += amount;
        while (saveData.xp >= saveData.xpToNextLevel)
        {
            saveData.level++;
            saveData.xp -= saveData.xpToNextLevel;
            levelledUp = true;
        }
        return levelledUp;
    }

    public static bool CheckNoEntryToday()
    {
        bool firstEntry = true;
        List<JournalEntry> journalEntries = saveData.journalEntries;
        if (journalEntries.Count > 0)
        {
            if (journalEntries[journalEntries.Count - 1].dateTime.Date == DateTime.Now.Date)
            {
                firstEntry = false;
            }
        }
        return firstEntry;
    }

}
