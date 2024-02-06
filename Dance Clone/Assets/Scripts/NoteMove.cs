using UnityEngine;

public class NoteMove : MonoBehaviour
{
    [Header("Speed Control")]
    [SerializeField]
    [Range(0.1f,10.0f)]
    private float noteSpeed = 3.0f;

    [Header("Dissapear Settings")]
    [SerializeField]
    private bool autoDisappear = true;
    [SerializeField]
    [Range(1.0f, 20.0f)]
    private float dissapearAfterSeconds = 10.0f;

    private void Update()
    {
        transform.Translate(Vector3.forward * noteSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Track End")
        {
            Debug.Log(gameObject.name + " destroyed");
            Destroy(gameObject);
        }
    }
}
