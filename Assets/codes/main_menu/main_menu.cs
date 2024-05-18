using UnityEngine.SceneManagement;
using UnityEngine;

public class main_menu : MonoBehaviour
{

    public string sceneName;
    public void StartButton()
    {
        SceneManager.LoadScene(sceneName);
    }

    public void quitButton()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif

    }
}
