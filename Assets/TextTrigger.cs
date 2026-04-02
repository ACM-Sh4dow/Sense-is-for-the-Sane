using UnityEngine;

public class TextTrigger : MonoBehaviour
{
    public GameObject text;
    public AudioSource audioSource;

    private void Start()
    {
        text.SetActive(false);
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        text.SetActive(true);
        audioSource.Play();
    }

    private void OnTriggerExit(Collider other)
    {
        text.SetActive(false);
    }
}
