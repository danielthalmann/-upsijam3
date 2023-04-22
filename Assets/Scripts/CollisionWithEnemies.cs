using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionWithEnemies : MonoBehaviour
{
	public LayerMask enemyLayerMask;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	void OnCollisionEnter2D(Collision2D col)
	{
		//Debug.Log(col.gameObject.layer);
		if (((1 << col.gameObject.layer) & enemyLayerMask.value) != 0)
		{
			//Debug.Log("hello");
		}
	}
}
