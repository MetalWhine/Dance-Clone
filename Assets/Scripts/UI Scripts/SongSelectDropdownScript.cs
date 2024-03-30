using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongSelectDropdownScript : MonoBehaviour
{
    public GameEvent OnChangeSong;

    private void OnEnable()
    {
        OnChangeSong.Raise(this, "Odo Scene");
    }

    public void ChangeLevel(int index)
    {
        switch (index)
        {
            case 0: OnChangeSong.Raise(this, "OdoScene"); break;
            case 1: OnChangeSong.Raise(this, "SomethingElse"); break;
        }
    }
}
