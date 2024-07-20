using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class theActualGameManger : MonoBehaviour
{
    public GameObject settingsWin;
    public bool isSound = true;
    public bool IsMusic = true;
    public int selectedMinutes;
    public GameObject mainMenu;
    public GameObject[] firstThreeTouchGivers;
    public Animator musicIcon;
    public Animator soundIcon;
    public bool isGameDone;
    private bool bossCalled = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            fin();
        }

        if (SceneManager.GetActiveScene().name == "game" && !bossCalled)
        {
            bossCalled = true;
            spawnTheBossHelper();
        }
    }
    public void handleClicksOnOne()
    {
        Invoke("settingsOutAnimation", .5f);
        selectMinutes(2);
    }
    public void handleClicksThree()
    {
        Invoke("settingsOutAnimation", .5f);
        selectMinutes(3);
    }
    public void handleClicksFive()
    {
        Invoke("settingsOutAnimation", .5f);
        selectMinutes(5);
    }

    public void handleClickOnSound()
    {
        if (isSound)
            soundIcon.GetComponent<Animator>().Play("offSound");
        else
            soundIcon.GetComponent<Animator>().Play("onSound");

        isSound = !isSound;

    }

    public void handleClickOnMusic()
    {
        if (IsMusic)
            musicIcon.GetComponent<Animator>().Play("offMusic");
        else
            musicIcon.GetComponent<Animator>().Play("onMusic");

        IsMusic = !IsMusic;

    }

    public void clickOnSettings()
    {
        if (isSound)
        {
            soundIcon.Play("onSound");
        }
        else
        {
            soundIcon.Play("offSound");
        }

        if (IsMusic)
        {
            musicIcon.Play("onMusic");
        }
        else
        {
            musicIcon.Play("offMusic");
        }
    }

    // private voids
    void settingsOutAnimation()
    {
        soundIcon.GetComponent<Animator>().Play("soundOut");
        musicIcon.GetComponent<Animator>().Play("musicOut");
        settingsWin.GetComponent<Animator>().Play("out");
        mainMenu.GetComponent<Animator>().Play("itemsIn");
        inActiveSettingsWindow();
    }

    void inActiveSettingsWindow()
    {
        settingsWin.SetActive(false);
        settingsWin.SetActive(false);
        foreach (var item in firstThreeTouchGivers)
        {
            item.SetActive(true);
        }
    }

    void selectMinutes(int target)
    {
        selectedMinutes = target;
    }
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void startTheGame()
    {
        Invoke("fin", selectedMinutes * 60);
    }

    public void spawnTheBossHelper()
    {
        spawnManager theSpawnManager = GameObject.Find("spawnManager").GetComponent<spawnManager>();
        theSpawnManager.bossSpawnHelper(selectedMinutes - 1);
    }

    public void fin()
    {
        isGameDone = true;
        GameObject finUI = GameObject.Find("finUI");
        finUI.GetComponent<fin>().done();
    }
}
