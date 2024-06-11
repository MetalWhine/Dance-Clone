using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneManagement : MonoBehaviour
{

    public static SceneManagement instance;

    public string targetScene;

    private string baseScene = "Odo Scene";
    private string difficulty = "Easy";

    public GameEvent changingScene;

    public bool isHandMode = false;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ChangeTargetScene(Component sender, object newTarget)
    {
        if(newTarget is string)
        {
            if(SceneManager.GetActiveScene().name == "Menu Scene")
            {
                baseScene = newTarget.ToString();
                targetScene = baseScene + " - " + difficulty;
                changingScene.Raise(this, targetScene);
            }
            else
            {
                targetScene = newTarget.ToString();
                changingScene.Raise(this, targetScene);
            }
        }
    }

    public void ModifyTargetScene(Component sender, object newTarget)
    {
        if (newTarget is string)
        {
            difficulty = newTarget.ToString();
            targetScene = baseScene + " - " + difficulty;
            Debug.Log(targetScene);
            changingScene.Raise(this, targetScene);
        }
    }

    public void QuitApplication()
    {
        Debug.Log("Quitting");
        Application.Quit();
    }

    public void LoadScene(){
        DataPersistenceManager.instance.SaveGame();
        int SceneIndex = SceneUtility.GetBuildIndexByScenePath(targetScene);
        if(SceneIndex != -1){ SceneManager.LoadSceneAsync(targetScene); }
        else { Debug.LogWarning("No scene called: " + targetScene + " Exists!"); }
    }

    public void LoadScene(string sceneName)
    {
        DataPersistenceManager.instance.SaveGame();
        int SceneIndex = SceneUtility.GetBuildIndexByScenePath(sceneName);
        if (SceneIndex != -1) { SceneManager.LoadSceneAsync(sceneName); }
        else { Debug.LogWarning("No scene called: " + sceneName + " Exists!"); }
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ChangeMode(bool handMode)
    {
        isHandMode = handMode;
    }
}
