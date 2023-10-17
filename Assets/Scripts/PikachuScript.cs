using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PikachuScript : MonoBehaviour
{
    public GameObject light;
    private float counter;

    // Update is called once per frame
    void Update()
    {
        counter += Time.deltaTime;
        if (counter > 0.8)
        {
            light.SetActive(!light.activeSelf);
            counter = 0;
        }
    }
}
