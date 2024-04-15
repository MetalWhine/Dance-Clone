using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class UIManager : MonoBehaviour, IDataPersistence
{
    public GameObject MenuUI;
    public GameObject LevelUI;
    public GameObject statsTextCont;
    public TMP_Text statsText;

    private Dictionary<string, int> score = null;
    private Dictionary<string, int> percent = null;

    public void LoadData(GameData data)
    {
        score = new Dictionary<string, int>();
        percent = new Dictionary<string, int>();
        percent = data.SongStatsPercent;
        score = data.SongStatsScore;
        foreach (var values in score.Values)
        {
            Debug.Log(values.ToString());
        }
    }

    public void SaveData(GameData data) 
    {
    }


    public void GoToMenuUI()
    {
        MenuUI.SetActive(true);
        LevelUI.SetActive(false);
        statsTextCont.SetActive(false);
        
    }

    public void GoToLevelUI()
    {
        MenuUI.SetActive(false);
        LevelUI.SetActive(true);
        statsTextCont.SetActive(true);

    }

    public void UpdateUI(Component sender, object newTarget)
    {
        int a = 0;
        int b = 0;
        Debug.Log(newTarget.ToString());
        try
        {
            score.TryGetValue(newTarget.ToString(), out a);
            percent.TryGetValue(newTarget.ToString(), out b);
            Debug.Log("Succesfully got scores!");
            statsText.text = "Best Score: " + a + "\n" +
                "Score Percent: " + b + "%";
        }
        catch (Exception e)
        {
            Debug.Log("Something went wrong: " + e);
            statsText.text = "Best Score: " + a + "\n" +
                "Score Percent: " + b + "%";
        }
    }
}
