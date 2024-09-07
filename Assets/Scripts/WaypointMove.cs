using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointMove : MonoBehaviour
{
    public List<GameObject> waypoints;  // List of waypoints
    public float speed = 1f;            // Movement speed
    public float delay = 3f;            // Delay before starting the movement
    public float pauseDuration = 2f;    // Duration to pause at each pause point
    public List<int> pauseIndices;      // List of waypoint indices where the player should pause

    private int index = 0;              // Current waypoint index
    private bool hasStarted = false;    // Flag to check if movement has started
    private bool isPausing = false;     // Flag to check if currently pausing

    private void Start()
    {
        // Start movement after the specified delay
        StartCoroutine(StartMovementAfterDelay());
    }

    private void Update()
    {
        if (!hasStarted || waypoints == null || waypoints.Count == 0 || isPausing)
            return;

        // Move towards the current waypoint
        MoveTowardsWaypoint();
    }

    private IEnumerator StartMovementAfterDelay()
    {
        // Wait for the initial delay before starting the movement
        yield return new WaitForSeconds(delay);
        hasStarted = true;
    }

    private void MoveTowardsWaypoint()
    {
        Vector3 destination = waypoints[index].transform.position;
        float step = speed * Time.deltaTime;

        // Move towards the waypoint
        transform.position = Vector3.MoveTowards(transform.position, destination, step);

        // Check if the object has reached the current waypoint
        if (Vector3.Distance(transform.position, destination) <= 0.05f)
        {
            // Check if the current index is in the list of pause indices
            if (pauseIndices.Contains(index))
            {
                // Start the pause coroutine
                StartCoroutine(PauseAtWaypoint());
            }
            else
            {
                // Move to the next waypoint
                if (index < waypoints.Count - 1)
                    index++;
            }
        }
    }

    private IEnumerator PauseAtWaypoint()
    {
        // Set pausing flag to true
        isPausing = true;

        // Wait for the specified pause duration
        yield return new WaitForSeconds(pauseDuration);

        // Set pausing flag to false and proceed to the next waypoint
        isPausing = false;
        if (index < waypoints.Count - 1)
            index++;
    }
}
