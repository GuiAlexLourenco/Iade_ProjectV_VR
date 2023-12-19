using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [HideInInspector] public GameObject player;
    public GameObject playerPrefab;

    public GameObject spawnLocation;

    void Awake()
    {
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            // Player already exists, so just set the Player GameObject parameter
            player = GameObject.FindGameObjectWithTag("Player");
        }
        else
        {
            // Instantiate the player and move it to the spawn location
            player = Instantiate(playerPrefab);
            player.transform.position = spawnLocation.transform.position;
        }
    }
}
