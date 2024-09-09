using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeTrigger : MonoBehaviour
{
    public EndFade endFade; // Reference to the ScreenFade script
    public Transform player; // Reference to the player object
    public float activationRange = 5f; // Range within which the fade starts
    public float delayBeforeStart = 2f; // Time in seconds to wait before starting the distance check
    private bool fadeTriggered = false; // To ensure the fade happens only once
    private bool delayCompleted = false; // To track if the delay is complete

    void Start()
    {
        // Start the coroutine that waits for the delay before allowing the distance check
        StartCoroutine(StartAfterDelay());
    }

    // Coroutine to wait for the specified delay before enabling the Update loop
    private IEnumerator StartAfterDelay()
    {
        yield return new WaitForSeconds(delayBeforeStart); // Wait for the specified delay
        delayCompleted = true; // Mark the delay as complete
    }

    void Update()
    {
        // Only check distance if the delay is complete and the fade hasn't been triggered
        if (delayCompleted && !fadeTriggered)
        {
            // Calculate the distance between the player and the object
            float distanceToPlayer = Vector3.Distance(player.position, transform.position);

            // If the player is within range, trigger the fade effect
            if (distanceToPlayer <= activationRange)
            {
                fadeTriggered = true; // Prevent the fade from being triggered multiple times
                StartCoroutine(endFade.FadeToWhite()); // Start the fade effect
            }
        }
    }
}
