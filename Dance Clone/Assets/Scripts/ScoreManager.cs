using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public AudioSource hitSFX;
    public AudioSource missSfX;
    public TMP_Text scoreText;
    static int comboScore;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        comboScore = 0;
    }

    public static void Hit()
    {
        Instance.hitSFX.Play();
        comboScore += 1;
    }
    
    public static void Miss()
    {
        Instance.missSfX.Play();
        comboScore = 0;
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = comboScore.ToString();
    }
}
