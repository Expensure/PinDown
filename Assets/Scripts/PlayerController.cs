using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    public float speed;
    public float airspeed;
    private GameObject focalPoint;
    private float fallThreshold = -50f;
    public float maxInputSpeed;
    public bool movementEnabled = true;
    public AudioClip ballRollSound;
    public AudioClip ballFallSound;
    public AudioClip fail;
    public AudioSource audioSource; 
    private bool wasGroundedLastFrame = true;

    private bool IsGrounded()
    {
        // Raycast downwards from the player's position
        RaycastHit hit;
        return Physics.Raycast(transform.position, Vector3.down, out hit, 1.5f);  
    }


    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (transform.position.y < fallThreshold)
        {
            
            audioSource.PlayOneShot(fail);
            Respawn();
            ResetVelocity();
        }
    }
    void FixedUpdate()
    {
        if (movementEnabled)
        {
            bool isGroundedNow = IsGrounded(); 

           
            if (!wasGroundedLastFrame && isGroundedNow)
            {
                if (ballFallSound != null)
                {
                    audioSource.PlayOneShot(ballFallSound);
                }
                else
                {
                    Debug.LogWarning("BallFallSound is not assigned!");
                }
            }

            wasGroundedLastFrame = isGroundedNow;

            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            if (isGroundedNow)
            {
                
                Vector3 force = (focalPoint.transform.right * horizontalInput + focalPoint.transform.forward * verticalInput) * speed;
                playerRb.AddForce(force);

                
                if (playerRb.linearVelocity.magnitude > 10f) 
                {
                    if (!audioSource.isPlaying || audioSource.clip != ballRollSound)
                    {
                        audioSource.clip = ballRollSound;
                        audioSource.loop = true;
                        audioSource.Play();
                    }
                }
                else
                {
                    audioSource.loop = false;
                }
            }
            else
            {
                
                Vector3 force = (focalPoint.transform.right * horizontalInput + focalPoint.transform.forward * verticalInput) * airspeed;
                playerRb.AddForce(force);
            }

            if (playerRb.linearVelocity.magnitude > maxInputSpeed)
            {
                playerRb.linearVelocity = playerRb.linearVelocity.normalized * maxInputSpeed;
            }
        }
    }


    // Function to reset all velocity to zero
    public void ResetVelocity()
    {
        playerRb.linearVelocity = Vector3.zero;      
        playerRb.angularVelocity = Vector3.zero; 
    }
    

    void Respawn()
    {
        
        if (Spawnpoint.lastSpawnPosition != Vector3.zero)
        {
            transform.position = Spawnpoint.lastSpawnPosition;
            Debug.Log("Player respawned at: " + Spawnpoint.lastSpawnPosition);
        }
        else
        {
            Debug.LogWarning("No spawn point activated yet!");
            transform.position = new Vector3(0.0f,0.5f,-20.0f);
        }
    }
}
