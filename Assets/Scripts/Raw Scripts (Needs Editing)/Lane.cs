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

    public void SetTimeStamps(Melanchall.DryWetMidi.Interaction.Note[] array)
    {
        foreach (var note in array)
        {
            if (note.NoteName == noteRestriction)
            {
                var metricTimeSpan = TimeConverter.ConvertTo<MetricTimeSpan>(note.Time, SongManager.midiFile.GetTempoMap());
                timeStamps.Add((double)metricTimeSpan.Minutes * 60f + metricTimeSpan.Seconds + (double)metricTimeSpan.Milliseconds / 1000f);
            }
        }
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

            //if (Input.GetKeyDown(input))
            //{
            //    if (Math.Abs(audioTime - timeStamp) < marginOfError)
            //    {
            //        HitEvent.Raise();
            //        print($"Hit on {inputIndex} note");
            //        Destroy(notes[inputIndex].gameObject);
            //        inputIndex++;
            //    }
            //    else
            //    {
            //        print($"Hit inaccurate on {inputIndex} note with {Math.Abs(audioTime - timeStamp)} delay");
            //    }
            //}
            if (timeStamp + marginOfError <= audioTime)
            {
                MissEvent.Raise();
                print($"Missed {inputIndex} note");
                inputIndex++;
            }
        }       
    
    }
}
