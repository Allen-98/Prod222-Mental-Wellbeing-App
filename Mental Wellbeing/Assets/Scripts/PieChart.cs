using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PieChart : MonoBehaviour
{
    public float[] values;
    public Color[] sectorColors;
    public Image[] legendColors;
    public Image sectorPrefab;
    public JournalEntry journalEntry;
    public Emoticon emoticon;
    public Text report;
    public Text legend;
    public System.DateTime lastSevenDays;
    
    //GameSave.saveData.journalEntries


    // Start is called before the first frame update
    void Start()
    {
        CreateChart();
        legend.text = "- Happy    \n- Sad    \n- Neutral    \n- Angry    \n- Anxious";
        LegendColorChanger();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void CreateChart()
    {
        float total = 0f;
        float z = 0f;
        string info;
        lastSevenDays = System.DateTime.Now.AddDays(-7);

        foreach (JournalEntry journalEntry in GameSave.saveData.journalEntries)
        {

            if (journalEntry.dateTime >= lastSevenDays)
            {
                //Debug.Log("within the past 7 days");
                if (journalEntry.emoticon == Emoticon.HAPPY)
                {
                    values[0] += 1;
                }
                else if (journalEntry.emoticon == Emoticon.SAD)
                {
                    values[1] += 1;
                }
                else if (journalEntry.emoticon == Emoticon.NEUTRAL)    
                {
                    values[2] += 1;
                }
                else if (journalEntry.emoticon == Emoticon.ANGRY)   
                {
                    values[3] += 1;
                }
                else if (journalEntry.emoticon == Emoticon.ANXIOUS)
                {
                    values[4] += 1;
                }            

            }

        }

        for (int i = 0; i < values.Length; i++)
        {
            total += values[i];
        }

        if (total > 0 & System.DateTime.Now >= lastSevenDays)
        {
            report.text = "During the past 7 days.....\n\n";
            info = " Happy:  " + values[0] + "\n Sad:  " + values[1] + "\n Neutral:  " + values[2] + "\n Angry:  " + values[3] + "\n Anxious:  " + values[4];
            report.text += info;
            report.text.ToString();
            for (int i = 0; i < values.Length; i++)
            {
                Image newSector = Instantiate(sectorPrefab) as Image;
                newSector.transform.SetParent(transform, false);
                newSector.color = sectorColors[i];
                newSector.fillAmount = values[i] / total;
                newSector.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, z));
                z -= newSector.fillAmount * 360f;
            }
        }

        //Debug.Log(total);


    }

    void LegendColorChanger() {
        legendColors[0].color = sectorColors[0];
        legendColors[1].color = sectorColors[1];
        legendColors[2].color = sectorColors[2];
        legendColors[3].color = sectorColors[3];
        legendColors[4].color = sectorColors[4];
    }


}
