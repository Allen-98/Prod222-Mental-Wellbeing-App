using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Breathing : MonoBehaviour
{
    public Button play;
    public float currentTime;
    public Text timerText;
    public Text breathingText;
    public bool isPressed;
    public string instruction;
    public int currentLength = 0;
    public float[] timingLengths;

    public Animator innerCircle;
    public Animator ring;
    public GameObject xpBarPopupPrefab;

    public Text readyText;
    private float maxReadyTextAlpha;


    // Start is called before the first frame update
    void Start()
    {
        currentTime = 0f;

        maxReadyTextAlpha = readyText.color.a;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPressed == true)
        {
            currentTime += 1 * Time.deltaTime;
            if (currentTime >= timingLengths[currentLength]) {
                currentTime = 0f;
        
                //if (currentLength == timingLengths.Length - 1)
                //{
                  //  isPressed = false;
                //}

                currentLength = (currentLength + 1) % timingLengths.Length;
                if (currentLength == 0) {
                    instruction = "Inhale";

                    bool levelledUp = GameSave.AddXP(5);
                    if (levelledUp && xpBarPopupPrefab != null) Instantiate(xpBarPopupPrefab);
                }
                else if (currentLength == 1)
                {
                    instruction = "Hold";
                }
                else {
                    instruction = "Exhale";
                }
            }

            timerText.text = Mathf.Ceil(timingLengths[currentLength] - currentTime).ToString();
            breathingText.text = instruction;

            Color colour = readyText.color;
            colour.a = 0f;
            readyText.color = colour;
            readyText.gameObject.SetActive(false);

        }
        else {
            PressedStop();

            timerText.text = "";
            breathingText.text = "";

            Color colour = readyText.color;
            colour.a = Mathf.Min(maxReadyTextAlpha, colour.a + (Time.deltaTime / 2f));
            readyText.color = colour;
            readyText.gameObject.SetActive(true);

        }

    
    }

    
    public void PressedPlay() {
        isPressed = true;
        currentTime = 0f;
        instruction = "Inhale";
        currentLength = 0;

        innerCircle.SetTrigger("Start");
        ring.SetTrigger("Start");
    }

    public void PressedStop() {
        currentTime = 0f;
        instruction = "Inhale";
        isPressed = false;

    }

    public void ToggleStartStop()
    {
        if (isPressed)
        {
            innerCircle.SetTrigger("Stop");
            ring.SetTrigger("Stop");
            PressedStop();
        }
        else
        {
            PressedPlay();
        }
    }

   
}


