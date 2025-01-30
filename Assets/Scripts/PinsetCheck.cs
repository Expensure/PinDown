using UnityEngine;

public class PinsetCheck : MonoBehaviour
{
    // public AudioClip strike;
    // private AudioSource audioSource;

    void Start()
    {
        // audioSource = GetComponent<AudioSource>();
    }
    
    void Update()
    {
            CheckPins();
    }


    void CheckPins()
    {
        if (transform.childCount == 0)
        {
            Debug.Log("All pins are destroyed.");
            // audioSource.PlayOneShot(strike);
            Destroy(gameObject);
        }
    }
}
