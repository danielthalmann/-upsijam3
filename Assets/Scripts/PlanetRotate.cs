using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetRotate : MonoBehaviour
{
    public float rotationSpeed = 10f;
    public ScoreCount counter;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float multiplier = ScoreCount.Instance.GetMultiplier();

        if (multiplier > 999)
        {
            multiplier = 999;
        }

        float multipliedRotationAmount = rotationSpeed + multiplier;

        // Change the rotation based on the current time
        float rotationAmount = (multipliedRotationAmount * Time.deltaTime);

        Debug.Log(Time.deltaTime);
        Debug.Log(rotationAmount);
        Debug.Log(multipliedRotationAmount);

        // Rotate the object
        transform.Rotate(0, 0, rotationAmount);
    }
}
