using UnityEngine;

[CreateAssetMenu(fileName = "LongNoteData", menuName = "ScriptableObjects/LongNotes")]
public class NoteDataSO : ScriptableObject
{
    public NoteData[] notes;
    public float songStartTime;
    public float moveSpeed;
}