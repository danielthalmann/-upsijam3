using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        float horizontal = Input.GetAxis("Horizontal") * Time.deltaTime;
        float vertical = Input.GetAxis("Vertical") * Time.deltaTime;

        Vector3 cartersian = new Vector3(vertical, -horizontal, 0);


        transform.position += cartersian2isometric(cartersian);

    }

    Vector3 cartersian2isometric(Vector3 cartersian)
    {
        Vector3 isometric = new Vector3();

        isometric.x = cartersian.x - cartersian.y;
        isometric.y = cartersian.x / 2 + cartersian.y / 2;
        isometric.z = cartersian.y;

        return isometric;

    }

}
