using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int PlayCount;
    public SerializableDictionary<string, int> SongStatsScore;
    public SerializableDictionary<string, int> SongStatsPercent;

    // Values in this constructor is the default values when there is no data to load
    public GameData()
    {
        PlayCount = 0;
        SongStatsScore = new SerializableDictionary<string, int>();
        SongStatsPercent = new SerializableDictionary<string, int>();
    }
}
