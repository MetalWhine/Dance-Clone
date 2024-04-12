using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelFinishedUI : MonoBehaviour
{
    public GameObject canvas1;
    public GameObject canvas2;

    public void ActivateCanvas()
    {
        canvas1.SetActive(true);
        canvas2.SetActive(true);
    }
}
