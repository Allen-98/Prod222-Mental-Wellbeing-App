using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class JournalEntry
{

    public Emoticon emoticon;
    public string text;
    public System.DateTime dateTime;

    public JournalEntry(Emoticon emoticon, string text)
    {
        this.emoticon = emoticon;
        this.text = text;
        dateTime = System.DateTime.Now;
    }

}
