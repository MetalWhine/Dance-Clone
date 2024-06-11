using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayModeTransformer : MonoBehaviour
{
    [SerializeField]
    private Transform handTransform;
    [SerializeField]
    private Transform feetTransform;

    private SceneManagement sceneManagement;

    private void OnEnable()
    {
        sceneManagement = FindObjectOfType<SceneManagement>();
        if (sceneManagement.isHandMode)
        {
            transform.position = handTransform.position;
            transform.rotation = handTransform.rotation;
        }
        else
        {
            transform.position = feetTransform.position;
            transform.rotation = feetTransform.rotation;
        }
    }
}
