using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using System.IO;
using UnityEngine.Networking;
using System;

// Notes that are used
// Keyboard shortcut: R, T, Y, U
// Keys: F, G, A, B

public class SongManager : MonoBehaviour, IDataPersistence
{
    public static SongManager Instance;

    [Header("Music Player")]
    public AudioSource audioSource;

    [Header("Midi File Location")]
    public string fileLocation;

    [Header("Lanes in Scene")]
    public Lane[] lanes;

    [Header("Timing Settings")]
    public float songDelayInSeconds;
    public double marginOfError; // in seconds
    public double perfectTiming; // in seconds
    public double goodTiming; // in seconds
    public int inputDelayInMilliseconds;

    [Header("Speed Settings [Higher numbers = slower]")]
    public float noteTime;

    [Header("Scoring Settings")]
    public int perfectScore = 500;
    public int goodScore = 300;
    public int okScore = 100;

    [Header("Note positioning data")]
    public float noteSpawnZ;
    public float noteTapZ;

    public GameEvent startSong;

    private int playCount;

    public void LoadData(GameData data)
    {
        this.playCount = data.PlayCount;
    }

    public void SaveData(ref GameData data)
    {
        data.PlayCount = this.playCount;
    }

    public float noteDespawnZ
    {
        get
        {
            return noteTapZ - (noteSpawnZ - noteTapZ);
        }
    }

    public static MidiFile midiFile;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        ReadFromFile();

    }

    private void ReadFromFile()
    {
        midiFile = MidiFile.Read(Application.streamingAssetsPath + "/" + fileLocation);
        GetDataFromMidi();
    }
    public void GetDataFromMidi()
    {
        var notes = midiFile.GetNotes();
        var array = new Melanchall.DryWetMidi.Interaction.Note[notes.Count];
        notes.CopyTo(array, 0);

        foreach (var lane in lanes) lane.SetTimeStamps(array);

        Invoke(nameof(StartSong), songDelayInSeconds);
    }
    public void StartSong()
    {
        audioSource.Play();
        startSong.Raise();
        playCount++;
    }

    public static double GetAudioSourceTime()
    {
        return (double)Instance.audioSource.timeSamples / Instance.audioSource.clip.frequency;
    }
}
