using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class XPBarPopup : MonoBehaviour
{
    //
    // Manages XP Bar popup
    //

    [Header("Settings")]
    [Tooltip("When true the player will be taken back to the homescreen after closing it.")]
    public bool gotoHomeOnClose = false;

    [Header("References")]
    public GameObject monitorLevelUpEffect;
    public Animator animator;

    private bool levelUp = false;

    void Start()
    {
        
    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Return)) GameSave.AddXP(20);

        if (!levelUp && monitorLevelUpEffect.activeSelf)
        {
            levelUp = true;
            if (GameSave.saveData.level <= 10)
            {
                animator.SetTrigger("Unlock");
            }
        }
    }

    public void Continue()
    {
        if (gotoHomeOnClose) SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

}
