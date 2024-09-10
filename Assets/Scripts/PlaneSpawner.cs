using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneSpawner : MonoBehaviour
{
    public Transform player; // Reference to the player
    public GameObject planePrefab; // Plane prefab to instantiate
    public Vector3 spawnOffset = new Vector3(0, 0, 5); // Offset for spawning the plane in front of the player
    public float triggerRange = 5f; // Range within which the plane will appear
    public Transform targetPoint; // The point the player needs to reach
    public float fadeDuration = 2f; // Duration of the fade-in effect
    public bool facePlayer = true; // Whether the plane should face the player
    public Vector3 customRotation = Vector3.zero; // Custom rotation in Euler angles if not facing the player
    public Vector3 targetScale = new Vector3(1, 1, 1); // The final scale of the plane after fade-in
    private bool planeSpawned = false; // To ensure the plane spawns only once

    void Update()
    {
        // Calculate the distance between the player and the target point
        float distanceToTarget = Vector3.Distance(player.position, targetPoint.position);

        // Check if the player is within the trigger range and the plane hasn't spawned yet
        if (distanceToTarget <= triggerRange && !planeSpawned)
        {
            StartCoroutine(SpawnAndFadeInPlane());
            planeSpawned = true; // Ensure it only spawns once
        }
    }

    IEnumerator SpawnAndFadeInPlane()
    {
        // Calculate where to spawn the plane in front of the player
        Vector3 spawnPosition = player.position + player.forward * spawnOffset.z + new Vector3(0, spawnOffset.y, 0);

        // Determine the rotation of the plane
        Quaternion planeRotation;
        if (facePlayer)
        {
            // Make the plane face the player by looking at the player's position
            Vector3 directionToPlayer = player.position - spawnPosition;
            planeRotation = Quaternion.LookRotation(-directionToPlayer.normalized);
        }
        else
        {
            // Use custom rotation if not facing the player
            planeRotation = Quaternion.Euler(customRotation);
        }

        // Instantiate the plane at the calculated position with the correct rotation
        GameObject spawnedPlane = Instantiate(planePrefab, spawnPosition, planeRotation);

        // Set the plane's initial scale to zero (for fade-in effect)
        spawnedPlane.transform.localScale = Vector3.zero;

        // Fade in the plane by increasing its scale over time
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float scaleFactor = Mathf.Clamp01(elapsedTime / fadeDuration);
            spawnedPlane.transform.localScale = Vector3.Lerp(Vector3.zero, targetScale, scaleFactor);

            yield return null;
        }

        // Ensure the plane is fully visible at the end of the fade
        spawnedPlane.transform.localScale = targetScale;
    }
}
