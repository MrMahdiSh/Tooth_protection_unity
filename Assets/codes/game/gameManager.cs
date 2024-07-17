using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class gameManager : MonoBehaviour
{
    public string mainMenuSceneName;
    public Text timerText;
    private float startTime = 5;
    private bool timerRunning;

    private spawnManager spawnManager;

    // Start is called before the first frame update
    void Start()
    {
        spawnManager = GameObject.Find("spawnManager").GetComponent<spawnManager>();
        StartCoroutine(CountdownRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.Escape))
        // {
        //     SceneManager.LoadScene(mainMenuSceneName);
        // }

        if (timerRunning)
        {
            float t = Time.time - startTime;

            string minutes = ((int)t / 60).ToString("00");
            string seconds = (t % 60).ToString("00");

            timerText.text = minutes + ":" + seconds;
        }

    }

    public Text readyText;
    public Text countdownText;

    public GameObject loader;

    IEnumerator CountdownRoutine()
    {
        readyText.text = "؟ﻦﯿﺘﺴﻫ ﻩﺩﺎﻣﺁ";
        countdownText.text = "5";

        yield return new WaitForSeconds(1f);
        countdownText.text = "4";
        yield return new WaitForSeconds(1f);
        countdownText.text = "3";
        readyText.text = "!ﻢﯾﺮﺑ ﻥﺰﺑ ﺲﭘ";
        yield return new WaitForSeconds(1f);
        countdownText.text = "2";
        yield return new WaitForSeconds(1f);
        countdownText.text = "1";
        yield return new WaitForSeconds(1f);
        timerRunning = true;

        loader.gameObject.SetActive(false); // Hide countdown after it's done
    }

}
