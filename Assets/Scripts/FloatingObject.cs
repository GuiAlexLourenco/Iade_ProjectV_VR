using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingObject : MonoBehaviour
{
    public Transform player; // Reference to the player object
    public float activationRange = 5f; // Range within which the object starts floating
    public float floatSpeed = 2f; // Speed of floating
    public float floatHeight = 1f; // Height to which the object floats
    private bool isFloating = false; // Check if the object is already floating
    private Vector3 initialPosition; // To store the initial position of the object

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
        }

        // If the object is floating, move it upwards
        if (isFloating)
        {
            // Smoothly move the object to the target height
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, initialPosition.y + floatHeight, transform.position.z), floatSpeed * Time.deltaTime);
        }
    }
}
