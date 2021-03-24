using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JournalEntryUI : MonoBehaviour
{

    [Header("References")]
    [Tooltip("Image component for the journal entry's emoticon.")]
    public Image imageEmoticon;
    [Tooltip("Text component for the journal entry's user text input.")]
    public Text textUserEntry;
    [Tooltip("Text component for the journal entry's date.")]
    public Text textDate;
    [Tooltip("RectTransform to expand date panel width to match text.")]
    public RectTransform dateRect;
    [Tooltip("How much padding to add to the sides of the date text.")]
    public float dateRectPadding = 8f;

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
