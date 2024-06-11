using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SongChecker : MonoBehaviour
{
    bool songPlaying = false;
    AudioSource audioSource;
    public GameEvent songOver;

    public void PlaySong() { songPlaying = true; audioSource = GetComponent<AudioSource>(); }


    private void Update()
    {
        if (songPlaying)
        {
            if (audioSource != null)
            {
                if (!audioSource.isPlaying)
                {
                    Debug.Log("Song Finished");
                    Invoke(nameof(finishSong), 1f);
                    songPlaying = !songPlaying;
                }
            }
        }
    }

    void finishSong() { songOver.Raise(this, "Menu Scene");
        Debug.Log("End song event sent");
    }
}
