using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using System.IO;
using UnityEngine.Networking;
using System;

public class SongManager : MonoBehaviour
{
    // To be able to access in other classes
    public static SongManager Instance;
    // Able to play music
    public AudioSource audioSource;
    // Delay the song for n amount of seconds
    public float songDelayInSeconds;
    // Adds input delay for n amount of milliseconds
    public float inputDelayInMilliseconds;
    // How much incorrect the player can be while still hitting the beat in seconds
    public double marginOfError;
    // Array of lanes
    public Lane[] lanes;

    // Location of where .midi files are kept
    public string fileLocation;
    // How long does the note stay on the scene
    public float noteTime;
    // Where notes are spawned
    public float noteSpawnX;
    // Where notes are supposed to hit
    public float noteTapX;
    // Where notes dissapear, set default to the distance equal to the spawn point to tap point
    public float noteDespawnX
    {
        get
        {
            return noteTapX - (noteSpawnX - noteTapX);
        }
    }

    // Reference to the actual midifile, stored here when its parsed
    public static MidiFile midifile;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        // Checks if streaming assets is a folder or website depending on platform
        if (Application.streamingAssetsPath.StartsWith("http://") || Application.streamingAssetsPath.StartsWith("https://"))
        {
            StartCoroutine(ReadFromWebsite());
        }
        else
        {
            ReadFromFile();
        }
    }

    // Reads off of a website if not on Windows/Mac
    private IEnumerator ReadFromWebsite()
    {
        using (UnityWebRequest www = UnityWebRequest.Get(Application.streamingAssetsPath + "/" + fileLocation))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.LogError(www.error);
            }
            else
            {
                byte[] results = www.downloadHandler.data;
                using (var stream = new MemoryStream(results))
                {
                    midifile = MidiFile.Read(stream);
                    GetDataFromMidi();
                }
            }
        };
    }

    // Reads midi files directly by finding the file
    private void ReadFromFile()
    {
        midifile = MidiFile.Read(Application.streamingAssetsPath + "/" + fileLocation);
        GetDataFromMidi();
    }

    private void GetDataFromMidi()
    {
        // Note data from .midi
        var notes = midifile.GetNotes();
        // Create an empty array to store data from the .midi in the format of the drywetmidi notes
        var array = new Melanchall.DryWetMidi.Interaction.Note[notes.Count];
        // Copy notes to the array
        notes.CopyTo(array, 0);

        // Set timestamp for each lane
        foreach(var lane in lanes)
        {
            lane.SetTimeStamps(array);
        }

        // Delay the start of song
        Invoke(nameof(StartSong), songDelayInSeconds);
    }

    public static double GetAudioSourceTime()
    {
        return (double)Instance.audioSource.timeSamples / Instance.audioSource.clip.frequency;
    }

    public void StartSong()
    {
        audioSource.Play();
    }
}
