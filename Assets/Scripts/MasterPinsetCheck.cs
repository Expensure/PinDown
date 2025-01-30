using UnityEngine;
using System.Collections;

public class MasterPinsetCheck : MonoBehaviour
{
    public GameObject nextLevel;
    public Camera POVcam;
    public Camera Flybycam;
    public GameObject Player;
    public bool started;
    private PlayerController playerController;
    public AudioClip Tada;
    private AudioSource audioSource;
    
    // public float fadeDuration = 2f; // Duration of fade effect
    // private Renderer[] renderers;

    void Start()
    {
        // Renderer[] renderers = nextLevel.GetComponentsInChildren<Renderer>();
        playerController = Player.GetComponent<PlayerController>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        CheckPins();
    }

    void CheckPins()
    {
        if (transform.childCount == 0 && started == false)
        {
            Debug.Log("All pins of the level are destroyed.");
            POVcam.enabled = false;
            Flybycam.enabled = true;
            nextLevel.SetActive(true);
            playerController.movementEnabled = false;
            playerController.ResetVelocity();
            started = true;
            playerController.audioSource.Stop();
            audioSource.PlayOneShot(Tada);
        }
    }
}