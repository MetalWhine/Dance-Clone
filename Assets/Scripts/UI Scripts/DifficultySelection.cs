using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultySelection : MonoBehaviour
{
    public GameEvent OnChangeDifficulty;

    private void Start()
    {
        OnChangeDifficulty.Raise(this, "Easy");
    }

    public void ChangeLevel(int index)
    {
        switch (index)
        {
            case 0: OnChangeDifficulty.Raise(this, "Easy"); break;
            case 1: OnChangeDifficulty.Raise(this, "Medium"); break;
            case 2: OnChangeDifficulty.Raise(this, "Hard"); break;
        }
    }
}
