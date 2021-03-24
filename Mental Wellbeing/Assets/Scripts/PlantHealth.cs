using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlantHealth : MonoBehaviour
{

    //
    // Controls plant health, growing it when it is under the rain and shrinking it when it is not
    //

    [Header("Plant Parts")]
    [Tooltip("The top plant piece.")]
    public SpriteRenderer plantTop;
    [Tooltip("The stem of the plant that is tiled.")]
    public SpriteRenderer plantStem;
    [Tooltip("How many units upwards to place the plant top.")]
    public float topOffset = 0f;

    [Header("Growth")]
    [Tooltip("The plant's health, 1 health translates to 1 unit in height.")]
    public float health = 1f;
    [Tooltip("Maximum plant health to win the game, determines the maximum height in units.")]
    public float maxHealth = 10f;
    [Tooltip("How many units per second the plant grows under optimal conditions.")]
    public float growSpeed = 0.5f;
    [Tooltip("How many units per second the plant shrinks when drooped.")]
    public float shrinkSpeed = 0.25f;
    [Tooltip("How long it takes for the plant to droop and begin shrinking.")]
    public float droopTime = 3f;

    private float droop = 0f;
    private float healthDelay = 1f;

    [Header("Cloud")]
    [Tooltip("The centre position of the cloud.")]
    public Transform cloudPosition;
    [Tooltip("How big the cloud's area of effect will be.")]
    public float cloudWidth = 1f;

    [Header("Events")]
    [Tooltip("Event to call when the plant is fully grown.")]
    public UnityEvent onWin;
    [Tooltip("Event to call when the plant dies.")]
    public UnityEvent onLose;

    [Header("Shader Settings")]
    [Tooltip("How many texels to droop the plant sides.")]
    public float droopMagnitude = 50f;
    [Tooltip("What colour to blend to when drooping.")]
    public Color droopColour = new Color(140, 90, 35);

    private bool finished = false;


    void Start()
    {
        plantTop.material.SetFloat("_DroopAmount", droopMagnitude);
        plantStem.material.SetFloat("_DroopAmount", -droopMagnitude);
        plantTop.material.SetColor("_DroopColour", droopColour);
        plantStem.material.SetColor("_DroopColour", droopColour);

        healthDelay = health;
        UpdateSprites();
    }

    void Update()
    {
        if (!finished)
        {
            
            float cloudEffectiveness = Mathf.Clamp((cloudWidth / 2f) - Mathf.Abs(cloudPosition.position.x - transform.position.x), 0f, 1f);
            if (cloudEffectiveness > 0.25f)
            {
                float growth = growSpeed * cloudEffectiveness;
                droop -= growth * Time.deltaTime;
            }
            else
            {
                droop += Time.deltaTime / droopTime;
            }
            
            if (droop < 0)
            {
                health += Mathf.Abs(droop);
            }
            else if (droop > 1f)
            {
                health -= shrinkSpeed * Time.deltaTime;
            }
            droop = Mathf.Clamp(droop, 0f, 1f);

            if (health <= 0.5f)
            {
                health = 0.5f;
                finished = true;
                onLose.Invoke();
                //Debug.Log("Plant died!");
            }
            else if (health >= maxHealth)
            {
                health = maxHealth;
                finished = true;
                onWin.Invoke();
                //Debug.Log("Plant fully grown!");
            }
        }

        healthDelay = Mathf.Lerp(healthDelay, health, Time.deltaTime * 2f);
        UpdateSprites();
        
    }

    private void UpdateSprites()
    {
        Vector2 topPosition = new Vector2(0, healthDelay + topOffset);
        float stemLength = Mathf.Max(healthDelay - 1f, 0f);
        plantTop.transform.localPosition = topPosition;
        plantStem.transform.localPosition = new Vector2(0, (stemLength / 2f) + 0.5f);
        plantStem.size = new Vector2(plantStem.size.x, stemLength);

        plantTop.material.SetFloat("_Droop", droop);
        plantStem.material.SetFloat("_Droop", droop);
    }

    public void AddHealth(float amount)
    {
        droop -= amount;
        if (droop < 0)
        {
            health += Mathf.Abs(droop);
        }
        droop = Mathf.Clamp(droop, 0f, 1f);
    }

    public bool IsFinished()
    {
        return finished;
    }
}
