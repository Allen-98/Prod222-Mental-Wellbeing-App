using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XPBarController : MonoBehaviour
{
    //
    // Controls xp bar stuff. yeah.
    //

    [Header("References")]
    [Tooltip("The Mask to adjust to reveal XP progress.")]
    public RectMask2D barInnerMask;
    [Tooltip("The Mask to adjust to reveal where XP progress will end up.")]
    public RectMask2D barBackMask;
    [Tooltip("The text to show the bar's level.")]
    public Text barLevel;
    [Tooltip("GameObject to set active when the player levels up (optional).")]
    public GameObject optionalLevelUpEffect;

    [Header("XP Bar Settings")]
    [Tooltip("How long it takes the bar to get to the destination.")]
    public float fillTime = 2f;
    [Tooltip("When true, the game will be saved when the XP bar is started.")]
    public bool saveOnStart = true;
    [Tooltip("Offset where from the left the bar should start filling.")]
    public float startFillOffset = 0f;

    private int startLevel = 0;
    private int currentLevel = 1;
    private int targetLevel = 1;
    private float startProgress = 0f;
    private float currentProgress = 0f;
    private float targetProgress = 0f;
    private float fillProgress = 0f;
    private int monitorXP = 0;

    void Start()
    {
        SetupProgress();

        UpdateComponents();

        if (saveOnStart) GameSave.Save();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)) GameSave.AddXP(5);

        if (monitorXP != GameSave.saveData.xp || targetLevel != GameSave.saveData.level) SetupProgress();
        
        fillProgress += Time.unscaledDeltaTime * (1f / fillTime);
        fillProgress = Mathf.Min(fillProgress, 1f);

        currentProgress = Mathf.Lerp(startProgress, targetProgress, Mathf.Pow(fillProgress, 0.5f));
        //if (Mathf.Abs(targetProgress - currentProgress) < 0.001f) currentProgress = targetProgress;

        currentLevel = startLevel + Mathf.FloorToInt(currentProgress);
        currentProgress = currentProgress % 1f;

        GameSave.currentProgress = currentProgress;
        GameSave.currentLevel = currentLevel;

        if (optionalLevelUpEffect != null && currentLevel > startLevel) optionalLevelUpEffect.SetActive(true);

        UpdateComponents();
    }

    private void UpdateComponents()
    {
        barLevel.text = currentLevel.ToString();
        
        Vector4 padding = barInnerMask.padding;
        padding.z = (1 - currentProgress) * (barInnerMask.rectTransform.rect.width - startFillOffset);
        barInnerMask.padding = padding;

        Vector4 paddingBack = barBackMask.padding;
        paddingBack.z = (currentLevel < targetLevel ? 0f : (1 - (targetProgress % 1f))) * (barBackMask.rectTransform.rect.width - startFillOffset);
        barBackMask.padding = paddingBack;
    }

    private void SetupProgress()
    {
        fillProgress = 0f;
        startLevel = GameSave.currentLevel;
        currentLevel = startLevel;
        startProgress = GameSave.currentProgress;
        currentProgress = startProgress;
        monitorXP = GameSave.saveData.xp;
        targetProgress = (float)GameSave.saveData.xp / GameSave.saveData.xpToNextLevel;
        targetLevel = GameSave.saveData.level;
        if (targetLevel > startLevel) targetProgress += targetLevel - startLevel;
    }
}
