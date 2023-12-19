using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    public Vector3 newPlayerPosition = new Vector3(1.9f, 0.87f, -1.16f);
    
    //public KeyCode sceneChangeKey = KeyCode.C;
    public float delayBeforeUnload = 0.5f;
    
    public string currentSceneName;
    public string nextSceneName;

    //void Update()
    //{
    //    ChangeScene();
    //}

    public void ChangeScene()
    {
        //if (Input.GetKeyDown(sceneChangeKey))
        //{
            // Find the player in the scene by checking for its tag
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            // Make the player object persistent
            DontDestroyOnLoad(player);

            // Move the player to the new position
            if (player != null)
            {
                player.transform.position = newPlayerPosition;
            }

            // Load the transition scene additively
            SceneManager.LoadSceneAsync(nextSceneName, LoadSceneMode.Additive);

            // Start a coroutine to unload the previous scene after a delay
            StartCoroutine(UnloadSceneAfterDelay(currentSceneName, delayBeforeUnload));
        //}
    }

    IEnumerator UnloadSceneAfterDelay(string sceneName, float delay)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Unload the current scene asynchronously
        SceneManager.UnloadSceneAsync(sceneName);
    }
}
