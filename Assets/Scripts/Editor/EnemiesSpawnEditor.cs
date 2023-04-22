
using UnityEditor;
using UnityEngine;
using System.Collections;

// Create a 180 degrees wire arc with a ScaleValueHandle attached to the disc
// that lets you modify the "radius" value in the WireArcExample
[CustomEditor(typeof(EnemiesSpawn))]
public class EnemiesSpawnEditor : Editor
{
    void OnSceneGUI()
    {
        Handles.color = Color.red;
        EnemiesSpawn myObj = (EnemiesSpawn)target;
        Handles.DrawWireArc(myObj.transform.position, Vector3.forward, myObj.transform.right, 180, myObj.radius);
        myObj.radius = (float)Handles.ScaleValueHandle(myObj.radius, myObj.transform.position + Vector3.up * myObj.radius, myObj.transform.rotation, 1, Handles.ConeHandleCap, 1);
    }
}