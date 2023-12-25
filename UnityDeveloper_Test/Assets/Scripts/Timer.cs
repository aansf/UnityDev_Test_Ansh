using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private float elapsedTime = 0f;
    public float targetTime = 120f; // 2 minutes in seconds
    public Text timerText; // Reference to your UI Text component

    void Update()
    {
        // Check if the game is still running
        if (elapsedTime < targetTime)
        {
            // Increment the elapsed time
            elapsedTime += Time.deltaTime;

            // Example: Display remaining time in the console
            float remainingTime = Mathf.Max(targetTime - elapsedTime, 0f);
            Debug.Log("Remaining Time: " + Mathf.Floor(remainingTime) + " seconds");

            // Update the UI Text component with the formatted time
            UpdateTimerUI(remainingTime);
        }
        else
        {
            // The timer has reached the target time (2 minutes)
            Debug.Log("Game Over - Time's up!");

            // You can add game over logic here
        }
    }

    void UpdateTimerUI(float remainingTime)
    {
        // Format the time as minutes:seconds
        string minutes = Mathf.Floor(remainingTime / 60).ToString("00");
        string seconds = (remainingTime % 60).ToString("00");

        // Update the UI Text component with the formatted time
        if (timerText != null)
        {
            timerText.text = minutes + ":" + seconds;
        }
    }
}
