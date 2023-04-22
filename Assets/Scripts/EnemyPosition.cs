using System;
using UnityEditor;
using UnityEngine;

public class EnemyPosition
{
    public GameObject instance = null;

    private float radius;
    private float speed;
    private Vector3 origin;

    public float current = 0;
    

    public EnemyPosition(GameObject obj, Vector3 origin, float radius, float speed)
    {
        
        this.instance = obj;
        this.radius = radius;
        this.origin = origin;
        this.speed = speed;

    }

    public bool IsGtRadius(float radius)
    {
        return (radius < this.radius);
    }

    public void Update()
    {
        current += Time.deltaTime * speed;

        Vector3 pos = ((new Vector3(Mathf.Cos((float)Math.PI / current), Mathf.Sin((float)Math.PI / current), 0)) * radius) + instance.transform.position;

        instance.transform.position = pos;

        Debug.Log("Update :" + current);

    }

}
