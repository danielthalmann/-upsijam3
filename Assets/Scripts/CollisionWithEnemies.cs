using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* maybe not here ?*/
using UnityEngine.SceneManagement;

using UnityEngine.Events;

public class CollisionWithEnemies : MonoBehaviour
{
	public LayerMask enemyLayerMask;
	public UnityEvent onPlayerDeath;

    public static int currentLife;

    public List<Image> lives;

    // Start is called before the first frame update
    void Start()
    {
        currentLife = lives.Count;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	void OnCollisionEnter2D(Collision2D col)
	{
        if (((1 << col.gameObject.layer) & enemyLayerMask.value) != 0)
		{
            RemoveLife();

            // x(maxLife), death
            if (currentLife < 1)
            {
                onPlayerDeath.Invoke();
            }
        }
	}

    void RemoveLife()
    {
        --currentLife;

        if (currentLife > 0)
        {
            lives[currentLife].enabled = false;
        }
    }
}
