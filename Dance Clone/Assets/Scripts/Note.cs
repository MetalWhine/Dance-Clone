using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    // When note is instantieated
    double timeInstantieated;
    // What time the player should tap the note
    public float assignedTime;

    // Start is called before the first frame update
    void Start()
    {
        // Gets the time from song manager
        timeInstantieated = SongManager.GetAudioSourceTime();
    }

    // Update is called once per frame
    void Update()
    {
        // Keeps track of the amount of time between when the note was first instantiated
        double timeSinceInstantiated = SongManager.GetAudioSourceTime() - timeInstantieated;
        // Keeps track of when it should be tapped or despwaned
        float t = (float)(timeSinceInstantiated / (SongManager.Instance.noteTime * 2));

        if (t > 1)
        {
            // Despawns if over despawn line (t => 1)
            Destroy(gameObject);
        }
        else
        {
            // Lerps the position between spawn and despawn coordinates (Spawn == t = 0)
            transform.position = Vector3.Lerp(Vector3.right * SongManager.Instance.noteSpawnX, Vector3.right * SongManager.Instance.noteDespawnX, t);
        }
    }
}
