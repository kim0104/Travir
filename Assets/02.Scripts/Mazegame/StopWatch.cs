
using UnityEngine;
using UnityEngine.UI;

public class StopWatch : MonoBehaviour
{
    public GameObject stopwatchCanvas;
    public Text stopwatchText;
    public Text clearTimeText;
    private float startTime;
    private bool isRunning = false;

    void Start()
    {
        stopwatchCanvas.SetActive(false);
        
    }

    public void Update()
    {
        if (stopwatchCanvas.activeSelf)
        {
            if (isRunning)
            {
                float elapsedTime = Time.time - startTime;
                string formattedTime = elapsedTime.ToString("F2");
                stopwatchText.text = formattedTime;
            }
        }
          
    }

    public void StartStopwatch()
    {
        if (!stopwatchCanvas.activeSelf)
        {
            
            stopwatchCanvas.SetActive(true);
        }

        isRunning = true;
        startTime = Time.time;
    }

    public void StopStopwatch()
    {
        isRunning = false;
        float elapsedTime = Time.time - startTime;
        clearTimeText.text = elapsedTime.ToString("F2");
    }

    public void ResetStopwatch()
    {
        isRunning = false;
        stopwatchCanvas.SetActive(false); 
    }
}

