using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongSelectDropdownScript : MonoBehaviour
{
    public GameEvent OnChangeSong;

    private void Awake()
    {
        OnChangeSong.Raise(this, "EZ");
    }

    public void ChangeLevel(int index)
    {
        switch (index)
        {
            case 0: OnChangeSong.Raise(this, "EZ"); break;
            case 1: OnChangeSong.Raise(this, "Spider Dance"); break;
            case 2: OnChangeSong.Raise(this, "Flashback"); break;
            case 3: OnChangeSong.Raise(this, "Death By Glamour"); break;
            case 4: OnChangeSong.Raise(this, "Midnight Amaretto"); break;
        }
    }
}
