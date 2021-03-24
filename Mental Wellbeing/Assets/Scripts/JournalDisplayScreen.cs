using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JournalDisplayScreen : MonoBehaviour
{

    [Header("References")]
    [Tooltip("Prefab for a journal entry.")]
    public GameObject journalEntryPrefab;
    [Tooltip("Prefab for an empty journal entry (has no user text).")]
    public GameObject journalEmptyEntryPrefab;
    [Tooltip("Content object in the viewport to parent journal entries to.")]
    public GameObject viewportContent;
    [Tooltip("Emoticon sprites.")]
    public EmoticonImages emoticonImages;

    private Dictionary<Emoticon, Sprite> emoticonSprites;

    void Start()
    {
        emoticonSprites = emoticonImages.MakeDictionary();
        FillEntries();
    }

    void Update()
    {
        
    }

    public void FillEntries()
    {
        // Clear children
        foreach (Transform child in viewportContent.transform)
        {
            Destroy(child.gameObject);
        }

        float offset = 15f;

        List<JournalEntry> entries = GameSave.saveData.journalEntries;

        for (int index = entries.Count - 1; index >= 0; index--)
        {
            JournalEntry entry = entries[index];

            GameObject entryPrefab = entry.text == "" ? journalEmptyEntryPrefab : journalEntryPrefab;
            GameObject entryObj = Instantiate(entryPrefab, viewportContent.transform);

            RectTransform rectTransform = entryObj.GetComponent<RectTransform>();
            Vector2 pos = rectTransform.anchoredPosition;
            pos.y = -offset;
            rectTransform.anchoredPosition = pos;
            offset += rectTransform.rect.height + 15f;

            JournalEntryUI entryUI = entryObj.GetComponent<JournalEntryUI>();
            entryUI.textDate.text = FormatDateTime(entry.dateTime);
            entryUI.textUserEntry.text = entry.text;
            entryUI.imageEmoticon.sprite = emoticonSprites[entry.emoticon];

            Vector2 dateSize = entryUI.dateRect.sizeDelta;
            dateSize.x = entryUI.textDate.preferredWidth + entryUI.dateRectPadding;
            entryUI.dateRect.sizeDelta = dateSize;
        }

        RectTransform rt = viewportContent.GetComponent<RectTransform>();
        Vector2 size = rt.sizeDelta;
        size.y = offset;
        rt.sizeDelta = size;

    }

    private string FormatDateTime(DateTime dateTime)
    {
        string result = ""; // dateTime.ToString("ddd d h:mmtt yyyy");
        if (dateTime.Date == DateTime.Today) result = "Today " + dateTime.ToString("h:mmt") + "m";
        else if (dateTime.Date > DateTime.Today.AddDays(-7).Date) result = dateTime.ToString("ddd h:mmt") + "m";
        else if (dateTime.Year == DateTime.Today.Year) result = dateTime.ToString("d MMM h:mmt") + "m";
        else result = dateTime.ToString("d MMM yyyy");
        return result;
    }

    [Serializable]
    public class EmoticonImages
    {
        public Sprite happy;
        public Sprite neutral;
        public Sprite sad;
        public Sprite angry;
        public Sprite anxious;

        public Dictionary<Emoticon, Sprite> MakeDictionary()
        {
            Dictionary<Emoticon, Sprite> dict = new Dictionary<Emoticon, Sprite>();
            dict[Emoticon.NONE] = null;
            dict[Emoticon.HAPPY] = happy;
            dict[Emoticon.NEUTRAL] = neutral;
            dict[Emoticon.SAD] = sad;
            dict[Emoticon.ANGRY] = angry;
            dict[Emoticon.ANXIOUS] = anxious;
            return dict;
        }
    }


}
