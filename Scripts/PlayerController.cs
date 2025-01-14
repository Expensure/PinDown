using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    public float speed = 5.0f;
    private GameObject focalPoint;
    // public bool hasPowerup;
    // public GameObject powerupIndicator;
    // public float powerupStrength;
    // public float powerupLength;
    public float maxInputSpeed;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");

        Vector3 force = focalPoint.transform.forward * speed * forwardInput;
        playerRb.AddForce(force);

        if (playerRb.linearVelocity.magnitude > maxInputSpeed)
        {
            playerRb.linearVelocity = playerRb.linearVelocity.normalized * maxInputSpeed;
        }

        // powerupIndicator.transform.position = transform.position + new Vector3(0, 0.5f, 0);
    }
    private void OnTriggerEnter(Collider other)
    {
        // if (other.CompareTag("Powerup"))
        // {
        //     hasPowerup = true;
        //     Destroy(other.gameObject);
        //     StartCoroutine(PowerupCountdownRoutine());
        //     powerupIndicator.gameObject.SetActive(true);
        // }
    }
    // IEnumerator PowerupCountdownRoutine()
    // {
        // yield return new WaitForSeconds(powerupLength);
        // hasPowerup = false;
        // powerupIndicator.gameObject.SetActive(false);
    // }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position);
            enemyRigidbody.AddForce(awayFromPlayer * 4, ForceMode.Impulse);
            // if (hasPowerup)
            // {
            //     Debug.Log("Collided with " + collision.gameObject.name + " with powerup set to " + hasPowerup);
            //     enemyRigidbody.AddForce(awayFromPlayer, ForceMode.Impulse);
            // }
            // else
            // {
            //     enemyRigidbody.AddForce(awayFromPlayer * 4, ForceMode.Impulse);
            // }
        }
        

            
    }
}
