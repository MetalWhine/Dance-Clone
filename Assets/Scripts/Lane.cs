using Melanchall.DryWetMidi.Interaction;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Notes that are used
// Keyboard shortcut: R, T, Y, U
// Keys: F, G, A, B

public class Lane : MonoBehaviour
{

    [Header("Lane settings")]
    [Space(10)]
    [Header("This value often changes for no reason, check often!")]
    [Tooltip("Which musical key is this lane assigned to?")]
    public Melanchall.DryWetMidi.MusicTheory.NoteName noteRestriction;
    [Space(10)]
    [Tooltip("Assign a note prefab to spawn here (It is key sensitive!)")]
    public GameObject notePrefab;

    List<Note> notes = new List<Note>();
    [HideInInspector]
    public List<double> timeStamps = new List<double>();

    int spawnIndex = 0;
    int inputIndex = 0;

    // Game Events when the note hits or misses
    public GameEvent HitEvent;
    public GameEvent MissEvent;

    // Game Event to count notes
    public GameEvent NoteCount;

    public void SetTimeStamps(Melanchall.DryWetMidi.Interaction.Note[] array)
    {
        int i = 0;
        foreach (var note in array)
        {
            if (note.NoteName == noteRestriction)
            {
                i++;
                var metricTimeSpan = TimeConverter.ConvertTo<MetricTimeSpan>(note.Time, SongManager.midiFile.GetTempoMap());
                timeStamps.Add((double)metricTimeSpan.Minutes * 60f + metricTimeSpan.Seconds + (double)metricTimeSpan.Milliseconds / 1000f);
            }
        }
        NoteCount.Raise(this, i);
    }
    // Update is called once per frame
    void Update()
    {
        if (spawnIndex < timeStamps.Count)
        {
            if (SongManager.GetAudioSourceTime() >= timeStamps[spawnIndex] - SongManager.Instance.noteTime)
            {
                var note = Instantiate(notePrefab, transform);
                notes.Add(note.GetComponent<Note>());
                note.GetComponent<Note>().assignedTime = (float)timeStamps[spawnIndex];
                spawnIndex++;
            }
        }

        if (inputIndex < timeStamps.Count)
        {
            double timeStamp = timeStamps[inputIndex];
            double marginOfError = SongManager.Instance.marginOfError;
            double audioTime = SongManager.GetAudioSourceTime() - (SongManager.Instance.inputDelayInMilliseconds / 1000.0);

            // Moved Input method to its own function

            if (timeStamp + marginOfError <= audioTime)
            {
                MissEvent.Raise();
                print($"Missed {inputIndex} note");
                inputIndex++;
            }
        }
    }

    public void NoteInput()
    {
        if (inputIndex < timeStamps.Count)
        {
            double timeStamp = timeStamps[inputIndex];
            double marginOfError = SongManager.Instance.marginOfError;
            double perfectTime = SongManager.Instance.perfectTiming;
            double goodTime = SongManager.Instance.goodTiming;

            int perfectScore = SongManager.Instance.perfectScore;
            int goodScore = SongManager.Instance.goodScore;
            int okScore = SongManager.Instance.okScore;

            double audioTime = SongManager.GetAudioSourceTime() - (SongManager.Instance.inputDelayInMilliseconds / 1000.0);

            if (Math.Abs(audioTime - timeStamp) < marginOfError)
            {
                if (Math.Abs(audioTime - timeStamp) < perfectTime)
                {
                    HitEvent.Raise(this, perfectScore);
                    print($"Perfect hit on {inputIndex} note");
                    Destroy(notes[inputIndex].gameObject);
                    inputIndex++;
                }
                else if (Math.Abs(audioTime - timeStamp) < goodTime)
                {
                    HitEvent.Raise(this, goodScore);
                    print($"Good hit on {inputIndex} note");
                    Destroy(notes[inputIndex].gameObject);
                    inputIndex++;
                }
                else
                {
                    HitEvent.Raise(this, okScore);
                    print($"Ok hit on {inputIndex} note");
                    Destroy(notes[inputIndex].gameObject);
                    inputIndex++;
                }
            }
            else
            {
                print($"Hit inaccurate on {inputIndex} note with {Math.Abs(audioTime - timeStamp)} delay");
            }
        }
    }
}