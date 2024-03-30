using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Important: Add input detection later

public class Note : MonoBehaviour
{
    [Header("Event called when note input")] // Game event when the note is hit
    [Tooltip("Remember to match the right events with the right notes!")]
    public GameEvent NoteHitEvent;
    // Time when note is instantiated
    double timeInstantiated;
    [HideInInspector] // Time designated for the player to hit the note
    public float assignedTime;

    void Start()
    {
        // Gets the time from song manager
        timeInstantiated = SongManager.GetAudioSourceTime();
    }

    void Update()
    {
        // Keeps track of the amount of time between when the note was first instantiated
        double timeSinceInstantiated = SongManager.GetAudioSourceTime() - timeInstantiated;
        // Keeps track of when it should be tapped or despwaned
        float t = (float)(timeSinceInstantiated / (SongManager.Instance.noteTime * 2));

        
        if (t > 1)
        {
            // Destroy when reaches despawn time (Time taken to reach despawn line)
            Destroy(gameObject);
        }
        else
        {
            // Lerps position from spawn line to despawn line
            transform.localPosition = Vector3.Lerp(Vector3.forward * SongManager.Instance.noteSpawnZ, Vector3.forward * SongManager.Instance.noteDespawnZ, t);
        }
    }
}
