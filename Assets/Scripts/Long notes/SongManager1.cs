using UnityEngine;

public class SongManager1 : MonoBehaviour
{
    private NoteData[] notes; // Array of NoteData objects representing the song
    public GameObject notePrefab; // Prefab for the note object

    public NoteDataSO noteDataSO;

    public void Start()
    {
        if(noteDataSO != null)
        {
            notes = noteDataSO.notes;
            SpawnNotes();
        }
    }

    void SpawnNotes()
    {
        foreach (var noteData in notes)
        {
            var noteObj = Instantiate(notePrefab, transform.position, transform.rotation);
            noteObj.GetComponent<LongNote>().SetNoteData(noteData, noteDataSO.songStartTime, noteDataSO.moveSpeed);
            noteObj.transform.parent = this.transform;
        }
    }
}
