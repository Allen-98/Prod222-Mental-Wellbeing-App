using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmoticonButton : MonoBehaviour
{

    [Header("Emoticon Button Settings")]
    [Tooltip("The emoticon the button represents.")]
    public Emoticon emoticon;
    [Tooltip("JournalAddEntryManager")]
    public JournalAddEntryManager manager;

    private Button button;

    void Start()
    {
        button = GetComponent<Button>();
    }

    void Update()
    {
        button.interactable = manager.selectedEmoticon != emoticon;
    }

    public void Clicked()
    {
        manager.EmoticonClicked(emoticon);
    }
}
