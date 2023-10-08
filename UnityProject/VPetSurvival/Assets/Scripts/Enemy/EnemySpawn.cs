using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawn : MonoBehaviour
{
    public GameObject Player;


    public float MaxPresence = 10f;
    public float SpawnInterval = 1f;

    public float SpawnRadius = 5f;

    public GameObject Enemy;

    private Transform enemyContainer;
    private float gameTimeElapsed = 0f;
    private float lastSpawnTime = 0f;
    private bool spawning = false;


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
        StartSpawning();
    }

    private void FixedUpdate()
    {
        doSpawning(Time.fixedDeltaTime);
    }


    public void StartSpawning()
    {
        spawning = true;
        spawnEnemy(Enemy);
    }

    public void StopSpawning(bool _reset = false)
    {
        spawning = false;

        if (_reset)
        {
            gameTimeElapsed = 0f;
        }
    }


    private void doSpawning(float _deltaTime)
    {
        if (!spawning)
        {
            return;
        }

        gameTimeElapsed += _deltaTime;

        float timeSinceLastSpawn = gameTimeElapsed - lastSpawnTime;
        if (timeSinceLastSpawn >= SpawnInterval)
        {
            spawnEnemy(Enemy);
        }
    }

    private void updatePresence()
    {
        // Determine the current max presence based on factors such as time and score
    }

    private void spawnEnemy(GameObject _enemy)
    {
        // Instantiate object
        GameObject newEnemy = GameObject.Instantiate(_enemy);
        newEnemy.transform.parent = enemyContainer;
        newEnemy.transform.position = getSpawnLocation();

        // Set movement target
        EnemyMovement movement = newEnemy.GetComponent<EnemyMovement>();
        movement.Target = Player;

        lastSpawnTime = Time.time;
    }

    private Vector3 getSpawnLocation()
    {
        float randomAngle = Random.value * 360f;
        Vector3 relativeSpawnVector = Quaternion.AngleAxis(randomAngle, Vector3.up) * Vector3.forward * SpawnRadius;
        Vector3 playerPosition = Player.transform.position;
        return playerPosition + relativeSpawnVector;
    }
}
