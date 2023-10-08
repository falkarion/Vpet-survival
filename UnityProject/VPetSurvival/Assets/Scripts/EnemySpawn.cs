using System;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{

    public float MaxPresence = 10f;
    public GameObject Enemy;

    private Transform enemyContainer;

    private void Awake()
    {
        enemyContainer = new GameObject("Enemy Container").transform;
        enemyContainer.transform.parent = this.transform;

        if (!Enemy)
        {
            throw new Exception("No enemy set");
        }
    }

    private void Start()
    {
        spawnEnemy(Enemy);
    }

    private void updatePresence()
    {
        // Determine the current max presence based on factors such as time and score
    }

    private void spawnEnemy(GameObject _enemy)
    {
        GameObject newEnemy = GameObject.Instantiate(_enemy);
        newEnemy.transform.parent = enemyContainer;
        newEnemy.transform.position = getSpawnLocation();
    } 

    private Vector3 getSpawnLocation()
    {
        return enemyContainer.transform.position;
    }
}
