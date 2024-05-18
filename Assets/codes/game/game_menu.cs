using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class game_menu : MonoBehaviour
{
    public string mainMenuSceneName;
    public void mainMenuClick()
    {
        SceneManager.LoadScene(mainMenuSceneName);
    }
}
