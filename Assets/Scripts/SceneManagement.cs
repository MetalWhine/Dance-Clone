using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    private string targetScene;

    public void ChangeTargetScene(Component sender, object newTarget)
    {
        if(newTarget is string)
        {
            Debug.Log(newTarget.ToString());
            targetScene = newTarget.ToString();
        }
    }

    public void QuitApplication()
    {
        Debug.Log("Quitting");
        Application.Quit();
    }

    public void LoadScene(){
        int SceneIndex = SceneUtility.GetBuildIndexByScenePath(targetScene);
        if(SceneIndex != -1){ SceneManager.LoadScene(targetScene); }
        else { Debug.LogWarning("No scene called: " + targetScene + " Exists!"); }
    }

    public void LoadScene(string sceneName)
    {
        int SceneIndex = SceneUtility.GetBuildIndexByScenePath(sceneName);
        if (SceneIndex != -1) { SceneManager.LoadScene(sceneName); }
        else { Debug.LogWarning("No scene called: " + sceneName + " Exists!"); }
    }

    public void LoadSceneAsync(string sceneName)
    {
        int SceneIndex = SceneUtility.GetBuildIndexByScenePath(sceneName);
        if (SceneIndex != -1) { SceneManager.LoadSceneAsync(sceneName); }
        else { Debug.LogWarning("No scene called: " + sceneName + " Exists!"); }
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
