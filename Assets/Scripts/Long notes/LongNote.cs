// TODO
// 1. Move the note from Z pos to Z neg (X)
// 2. Move only when start time (X)
// 3. Set note length to duration (X)
// 4. Despawn after reaching end (X)
// 5. Connect with scoring system ()

using UnityEngine;

public class LongNote : MonoBehaviour
{
    private bool _isHeld = false;
    private bool _isHoldable = false;
    private float _startTime;
    private float _endTime;
    private float _score;
    private float _moveSpeed;
    private bool transformed = false;

    public GameEvent missEvent;
    public GameEvent hitEvent;

    void Start()
    {
        _score = 0f;
    }

    public void SetNoteData(NoteData noteData, float songStartTime, float moveSpeed)
    {
        _startTime = songStartTime + noteData.startTime;
        _endTime = _startTime + noteData.length;
        _moveSpeed = moveSpeed;
    }

    void Update()
    {
        // Check if time
        // Then check if held
        // Move during time
        if(Time.time >= _startTime)
        {
            transform.position += transform.forward * _moveSpeed * Time.deltaTime; // Move forward
            if (!transformed)
            {
                lengthenNote();
            }
        }

        if (_isHeld)
        {
            if (Time.time > _endTime)
            {
                Debug.Log("Note let go");
                ReleaseNote();
            }
            else
            {
                Debug.Log("Adding score to note");
                _score += Time.deltaTime;
            }
        }
        
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entered trigger");
        if(other.tag == "Interactible Area")
        {
            _isHoldable = true;
        }
        if(other.tag == "Player" && _isHoldable)
        {
            Debug.Log("Entered Player");
            if (!_isHeld)
            {
                _isHeld = true;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.tag == "Untagged")
        {
            return;
        }
        if (_isHeld && _isHoldable)
        {
            Debug.Log("Finished Long Note");
            ReleaseNote();
        }
        else if (_isHoldable)
        {
            Debug.Log("Missed Long Note");
            missEvent.Raise();
            Destroy(this.gameObject);
        }
    }

    private void ReleaseNote()
    {
        _isHeld = false;

        int i = ((int)Mathf.Round(_score)+1) * 100;
        hitEvent.Raise(this, i);

        Destroy(this.gameObject);
    }

    private void lengthenNote()
    {
        transformed = true;
        transform.localScale = new Vector3(0.5f, 0.05f, _endTime - _startTime);
        transform.localPosition -= new Vector3(0f, 0f, (_endTime - _startTime)/2);
    }
}
