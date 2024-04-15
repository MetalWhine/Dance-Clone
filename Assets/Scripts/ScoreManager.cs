using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour, IDataPersistence
{
    public static ScoreManager Instance;
    public AudioSource hitSFX;
    public AudioSource missSFX;
    public TMP_Text scoreText;
    public TMP_Text comboText;
    static int comboScore = 0;
    static int totalScore = 0;
    static int hits = 0;
    static int highestCombo = 0;

    private int noteCount = 0;

    public TMP_Text endScoreText;

    private int bestScore;
    private int bestPercent;

    public void LoadData(GameData data)
    {
        string scene = SceneManager.GetActiveScene().name;
        data.SongStatsScore.TryGetValue(scene, out bestScore);
        data.SongStatsPercent.TryGetValue(scene, out bestPercent);
    }

    public void SaveData(GameData data)
    {
        string scene = SceneManager.GetActiveScene().name;
        if (data.SongStatsScore.ContainsKey(scene))
        {
            data.SongStatsScore.Remove(scene);
        }        
        if (data.SongStatsPercent.ContainsKey(scene))
        {
            data.SongStatsPercent.Remove(scene);
        }
        data.SongStatsScore.Add(scene, bestScore);
        data.SongStatsPercent.Add(scene, bestPercent);
    }

    void Start()
    {
        Instance = this;
        comboScore = 0;
        totalScore = 0;
    }
    public static void hit(Component x, object i)
    {
        if(i is int)
        {
            totalScore += (int)i;
            comboScore += 1;
            hits += 1;
            if(comboScore > highestCombo)
            {
                highestCombo = comboScore;
            }
        }
        //instance.hitsfx.play();
    }
    public static void Miss()
    {
        comboScore = 0;
        //Instance.missSFX.Play();
    }
    private void Update()
    {
        scoreText.text = "Score: " + totalScore.ToString();
        comboText.text = "Combo: " + comboScore.ToString();
    }

    public void addNoteCount(Component x, object data)
    {
        if (data is int)
        {
            noteCount += (int)data;
        }
    }

    public void SongEnd()
    {
        float i = Mathf.Round(((float)totalScore / ((float)noteCount * 500f)) * 100);
        endScoreText.text = "Score: " + totalScore.ToString() +"\n" +
            "High Combo: " + highestCombo + "\n" +
            "Notes: " + hits + "/" + noteCount + "\n" +
            "Score Percent: " + i + "%";
        if(bestScore < totalScore)
        {
            bestScore = totalScore;
        }
        if(bestPercent < i)
        {
            bestPercent = (int)i;
        }
    }
}
