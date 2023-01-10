using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timerText;
    private float startTime;
    public float simulatedTime = 0;

    public Button timerButton;
    public float timeFromButton = 0;
    public bool buttonPressed = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        startTime = Time.time;
        string minutes = ((int)startTime / 60).ToString();
        string seconds = (startTime % 60).ToString("f1");       //"f0" means no nachkommastellen, "f1" means 1 nachkommastelle

        timerText.text = minutes + ":" + seconds;
        timerButton.GetComponentInChildren<Text>().text = timeFromButton.ToString("f1");
        StartButtonTimer();
    }

    public void StartButtonTimer()
    {
        if (buttonPressed == true)
        {
            timeFromButton += Time.deltaTime;
        }
    }

    public void PressTimeButton()
    {
        buttonPressed = !buttonPressed;
    }
}
