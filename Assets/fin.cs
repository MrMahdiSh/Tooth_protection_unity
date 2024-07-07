using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class fin : MonoBehaviour
{
    public GameObject UI;

    public void done()
    {
        UI.SetActive(true);
        spawnManager theSpawn = GameObject.Find("spawnManager").GetComponent<spawnManager>();
        theSpawn.spawn = false;
        Time.timeScale = .1f;
    }
    public void reset()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("game");
    }
    public void home()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("main_menu");
    }
}
