using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundStart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("collision avec " + collision.gameObject.name);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("collision acvec " + other.gameObject.name);
        if (other.gameObject.tag == "SoundStart")
        {
            GetComponent<AudioSource>().Play();
        }
    }

}