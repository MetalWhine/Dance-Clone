using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{

    public void QuitApplication()
    {
        Debug.Log("Quitting");
        Application.Quit();
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadSceneAsync(string sceneName)
    {
        // Load scene asynchronously for smoother transitions
        SceneManager.LoadSceneAsync(sceneName);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
