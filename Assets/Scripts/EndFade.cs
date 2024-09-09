using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndFade : MonoBehaviour
{
    public float fadeDuration = 3f; // Duration of the fade effect
    public float imageDelay = 2f; // Time in seconds to wait before starting the image fade
    public float imageFadeDuration = 1f; // Duration of the image fade-in effect
    public CanvasGroup imageCanvasGroup; // Reference to the CanvasGroup on the Image

    private CanvasGroup canvasGroup;

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0; // Make sure the screen is transparent at the start

        // Make sure the image is initially hidden by setting its alpha to 0
        if (imageCanvasGroup != null)
        {
            imageCanvasGroup.alpha = 0;
            imageCanvasGroup.gameObject.SetActive(false); // Initially deactivate the image
        }
    }

    // Coroutine to handle the fade to white
    public IEnumerator FadeToWhite()
    {
        float timer = 0f;

        while (timer <= fadeDuration)
        {
            timer += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(0, 1, timer / fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = 1; // Ensure the screen is fully white at the end

        // Start another coroutine to show the image with a fade after a delay
        StartCoroutine(FadeInImageAfterDelay());
    }

    // Coroutine to fade in the image after the screen fade and a delay
    private IEnumerator FadeInImageAfterDelay()
    {
        yield return new WaitForSeconds(imageDelay); // Wait for the specified delay

        // Activate the image before fading it in
        if (imageCanvasGroup != null)
        {
            imageCanvasGroup.gameObject.SetActive(true); // Activate the image

            float timer = 0f;

            while (timer <= imageFadeDuration)
            {
                timer += Time.deltaTime;
                imageCanvasGroup.alpha = Mathf.Lerp(0, 1, timer / imageFadeDuration);
                yield return null;
            }

            imageCanvasGroup.alpha = 1; // Ensure the image is fully opaque at the end
        }
    }
}
