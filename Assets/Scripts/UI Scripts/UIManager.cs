using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject MenuUI;
    public GameObject LevelUI;

    public void GoToMenuUI()
    {
        MenuUI.SetActive(true);
        LevelUI.SetActive(false);
    }

    public void GoToLevelUI()
    {
        MenuUI.SetActive(false);
        LevelUI.SetActive(true);
    }
}
