using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetRotate : MonoBehaviour
{
    public float rotationSpeed = 10f; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Change the rotation based on the current time
        float rotationAmount = rotationSpeed * Time.deltaTime;

        // Rotate the object
        transform.Rotate(0, 0, -rotationAmount);
}
}
