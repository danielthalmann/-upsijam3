using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemiesSpawn : MonoBehaviour
{

    public float radius = 1f;
    public float frequence = 5f;
    public float speed = 5f;

    public List<GameObject> enemies;


    private List<EnemyPosition> instances = new List<EnemyPosition>();
    private float timerElapsed = 0;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        timerElapsed += Time.deltaTime;

        if (timerElapsed > frequence)
        {
            if (enemies.Count > 0)
            {
                int index = UnityEngine.Random.Range(0, enemies.Count);

                GameObject newEnemyObject = Instantiate(enemies[index], transform);

                EnemyPosition enemy = new EnemyPosition(newEnemyObject, transform.position, radius, speed);

                instances.Add(enemy);

            }
            timerElapsed = 0;
        }

        for (int i = 0; i < instances.Count; i++)
        {
            EnemyPosition enemy = instances[i];


            if (enemy.IsGtRadius(Mathf.PI) )
            {
               
                Destroy(enemy.instance);
                instances.Remove(enemy);
            } 
            else
            {
                enemy.Update();
            }

            

        }

    }
}