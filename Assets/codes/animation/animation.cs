using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class animation : MonoBehaviour
{
    public float countdownTime = 3f; // Adjust as needed
    private float currentTime;

    public string nextSceneText;

    void Start()
    {
        currentTime = countdownTime;
        InvokeRepeating("Countdown", 1f, 1f); // Start counting down every second
    }

    void Countdown()
    {
        currentTime -= 1f;

        if (currentTime <= 0)
        {
            ChangeScene();
        }
    }

    void ChangeScene()
    {
        // Change "YourSceneName" to the actual name of your scene
        SceneManager.LoadScene(nextSceneText);
    }
}
