using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FloatingObject : MonoBehaviour
{
    public Transform player; // Reference to the player object
    public float activationRange = 5f; // Range within which the object starts floating
    public float floatSpeed = 2f; // Speed of floating
    public float floatHeight = 1f; // Height to which the object floats
    public float rotationSpeed = 50f; // Speed of rotation (degrees per second)
    public float bobbingAmplitude = 0.5f; // Amplitude of the bobbing effect (how high and low it moves)
    public float bobbingFrequency = 2f; // Frequency of the bobbing effect (how fast it moves up and down)

    private bool isFloating = false; // Check if the object is already floating
    private Vector3 initialPosition; // To store the initial position of the object

    public UnityEvent OnPlayerInRange; // UnityEvent that can be set up in the inspector

    void Start()
    {
        // Store the initial position of the object
        initialPosition = transform.position;
    }

    void Update()
    {
        // Calculate distance between player and object
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);

        // If the player is within the activation range and the object is not floating yet
        if (distanceToPlayer <= activationRange && !isFloating)
        {
            isFloating = true; // Start floating
            OnPlayerInRange.Invoke(); // Trigger the UnityEvent when the player enters the range
        }

        // If the object is floating, move it upwards and apply bobbing effect
        if (isFloating)
        {
            // Apply bobbing effect using a sine wave
            float bobbingOffset = Mathf.Sin(Time.time * bobbingFrequency) * bobbingAmplitude;

            // Set the new position with bobbing and floating
            Vector3 newPosition = new Vector3(transform.position.x, initialPosition.y + floatHeight + bobbingOffset, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, newPosition, floatSpeed * Time.deltaTime);

            // Rotate the object around the Y-axis
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }
    }
}
