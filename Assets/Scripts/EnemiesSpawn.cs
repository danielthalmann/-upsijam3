using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemiesSpawn : MonoBehaviour
{

    public float radius = 1f;
    public float frequence = 3f;
	public float minFrequence = 1f;
	public float maxFrequence = 5f;
    public float speed = 5f;

    public List<GameObject> enemies;


    private List<EnemyPosition> instances = new List<EnemyPosition>();
    private float timerElapsed = 0;

    public ScoreCount counter;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        timerElapsed += Time.deltaTime;

        float multiplier = ScoreCount.Instance.GetMultiplier();

        if (multiplier > 999)
        {
            multiplier = 999;
        }

        float multipliedSpeed = speed + (multiplier * 2 / 100);

        //Debug.Log("Enemies:");
        //Debug.Log(multipliedSpeed);


        if (timerElapsed > frequence)
        {
            if (enemies.Count > 0)
            {
                int index = UnityEngine.Random.Range(0, enemies.Count);
                GameObject newEnemyObject = Instantiate(enemies[index], transform);
                EnemyPosition enemy = new EnemyPosition(newEnemyObject, transform.position, radius, multipliedSpeed);

                instances.Add(enemy);
            }
            timerElapsed = 0;

			/* compute random frequence in between a range */
			System.Random rand = new System.Random();
			frequence = minFrequence + (float)rand.NextDouble() * (maxFrequence - minFrequence);
			Debug.Log("frequence: "+frequence);
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
