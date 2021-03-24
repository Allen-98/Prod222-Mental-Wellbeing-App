using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WelcomeManager : MonoBehaviour
{
    //
    // Manages things relating to welcoming the player :D
    //

    [Header("References")]
    [Tooltip("Prefab to instantiate when after okay button is clocked.")]
    public GameObject addJournalEntryPrefab;

    public void ClickOkay()
    {
        Instantiate(addJournalEntryPrefab);
        Destroy(gameObject);
    }

}
