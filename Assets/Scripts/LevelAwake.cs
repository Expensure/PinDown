using UnityEngine;
using System.Collections;

public class LevelAwake : MonoBehaviour
{
    public Camera POVcam;
    public Camera lastcam;
    public GameObject Player;
    private PlayerController playerController;
    public float cameraTime = 3.0f; // Duration before switching cameras

    void Awake()
    {
        playerController = Player.GetComponent<PlayerController>();
        StartCoroutine(TransitionToPOVCam());
    }

    private IEnumerator TransitionToPOVCam()
    {
        yield return new WaitForSeconds(cameraTime);  

        // Debugging logs to check if cameras are assigned properly
        Debug.Log("Transitioning cameras...");
        
        lastcam.enabled = false;
        POVcam.enabled = true;
        playerController.movementEnabled = true;
    }
}
