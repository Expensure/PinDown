using UnityEngine;

public class RotationChecker : MonoBehaviour
{
    private Material objectMaterial;
    private bool isFading = false;
    private Color pinColor;
    private float alphaf;
    public float fadeSpeed = 1.0f; 
    private AudioSource audioSource;
    private bool soundPlayed = false; 

    void Start()
    {
        pinColor = this.GetComponent<MeshRenderer>().material.color;
        alphaf = pinColor.a;
        audioSource = GetComponent<AudioSource>(); 
           }

    void Update()
    {
        // Check if pin falls by checking rotation
        Vector3 rotation = transform.eulerAngles;
        if (!isFading && (rotation.x > 60 || rotation.y > 60 || rotation.z > 60))
        {
            isFading = true;
            StartCoroutine(FadeOutAndDestroy());
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!soundPlayed && (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Pin"))) 
        {
            audioSource.Play();
            soundPlayed = true;
        }
    }

    private System.Collections.IEnumerator FadeOutAndDestroy()
    {

        while (alphaf > 0.0f)
        {
            alphaf -= Time.deltaTime / fadeSpeed;

            this.GetComponent<MeshRenderer>().material.color = new Color(pinColor.r, pinColor.g, pinColor.b, alphaf);
            yield return null;
        }

        Destroy(gameObject);
    }
}
