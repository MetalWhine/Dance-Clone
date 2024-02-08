using Melanchall.DryWetMidi.Interaction;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lane : MonoBehaviour
{
    // Notes that are used
    // Keyboard shortcut: R, T, Y, U
    // Keys: F, G, A, B

    // Restricts notes to a certain key
    public Melanchall.DryWetMidi.MusicTheory.NoteName noteRestriction;
    // Input (for testing only)
    public KeyCode input;
    // Reference for the note prefab
    public GameObject notePrefab;
    // Keeps track of all notes
    List<Note> notes = new List<Note>();
    // Every timestamp that the player needs to tap
    public List<double> timeStamps = new List<double>();

    // Keeps track of which note to be spawned
    int spawnIndex = 0;
    // Keeps track of which timestamp needs to be checked
    int inputIndex = 0;

    public void SetTimeStamps(Melanchall.DryWetMidi.Interaction.Note[] array)
    {
        foreach (var note in array)
        {
            if (note.NoteName == noteRestriction)
            {
                // Since note.Time does not properly return the timestamp (uses .midi timestamp), we need a converter
                // Converts to metric time stamps
                var metricTimeSpan = TimeConverter.ConvertTo<MetricTimeSpan>(note.Time, SongManager.midifile.GetTempoMap());
                // Adds the metric timestamps to list of timeStamps
                timeStamps.Add((double)metricTimeSpan.Minutes * 60.0f + metricTimeSpan.Seconds + (double)metricTimeSpan.Milliseconds / 1000.0f);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnIndex < timeStamps.Count)
        {
            // Want to spawn the note [noteTime] seconds before the tap time
            if (SongManager.GetAudioSourceTime() >= timeStamps[spawnIndex] - SongManager.Instance.noteTime)
            {
                // Instantiates the note
                var note = Instantiate(notePrefab, transform);
                // Puts note in list to keep track of it
                notes.Add(note.GetComponent<Note>());
                // The note know the location of where its supposed to be itself
                note.GetComponent<Note>().assignedTime = (float)timeStamps[spawnIndex];
                // Increase spawn index to keep track of notes
                spawnIndex++;
            }
        }

        if (inputIndex < timeStamps.Count)
        {
            double timeStamp = timeStamps[inputIndex];
            double marginOfError = SongManager.Instance.marginOfError;
            double audioTime = SongManager.GetAudioSourceTime() - (SongManager.Instance.inputDelayInMilliseconds / 1000.0f);
            
            //if (Input.GetKeyDown(input))
            //{
            //    // Checks if note is in position with margin of error taken into account
            //    if (Math.Abs(audioTime - timeStamp) < marginOfError)
            //    {
            //        Hit();
            //        print($"Hit on {inputIndex} note");
            //        Destroy(notes[inputIndex].gameObject);
            //        inputIndex++;
            //    }
            //    else
            //    {
            //        print($"Hit innacurate on {inputIndex} note");
            //    }
            //}
            if (timeStamp + marginOfError <= audioTime)
            {
                Miss();
                print($"Hit missed on {inputIndex} note");
            }
        }
    }

    private void Hit()
    {
        ScoreManager.Hit();
    }

    private void Miss()
    {
        ScoreManager.Miss();
    }
}
