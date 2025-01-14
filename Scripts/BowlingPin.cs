using UnityEngine;

public class RotationChecker : MonoBehaviour
{
    private Material objectMaterial;
    private bool isFading = false;
    private Color pinColor;
    private float alphaf;
    public float fadeSpeed = 1.0f; //Time in seconds after which alphaf needs to be 0

    void Start()
    {
        pinColor = this.GetComponent<MeshRenderer>().material.color;
        alphaf = pinColor.a;
    }

    void Update()
    {
        // Check if any rotation exceeds 30 degrees
        Vector3 rotation = transform.eulerAngles;
        if (!isFading && (rotation.x > 60 || rotation.y > 60 || rotation.z > 60))
        {
            isFading = true;
            StartCoroutine(FadeOutAndDestroy());
        }
    }

    private System.Collections.IEnumerator FadeOutAndDestroy()
    {

        while (alphaf > 0.0f)
        {
            alphaf -= Time.deltaTime / fadeSpeed; // Smooth fade based on fadeSpeed

            this.GetComponent<MeshRenderer>().material.color = new Color(pinColor.r, pinColor.g, pinColor.b, alphaf);
            yield return null;
        }

        Destroy(gameObject);
    }
}
