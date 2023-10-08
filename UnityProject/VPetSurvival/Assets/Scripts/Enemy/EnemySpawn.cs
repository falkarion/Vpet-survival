using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawn : MonoBehaviour
{
    private const int SPAWN_LOOP_CAP = 250;
    private const int DEFAULT_MAX_PRESENCE = 10;

    public GameObject Player;
    public GameObject Enemy;

    public int MaxPresence = 10;
    public int CurrentPresence = 0;
    public int MaxPresenceIncreasePerMinute = 30;

    public float SpawnInterval = 1f;
    public float SpawnRadius = 5f;

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
        spawning = true;
        MaxPresence = DEFAULT_MAX_PRESENCE;
    }

    private void FixedUpdate()
    {
        determineMaxPresence();
        doSpawn(Time.fixedDeltaTime);
    }

    private void doSpawn(float _deltaTime)
    {
        if (!spawning)
        {
            return;
        }

        gameTimeElapsed += _deltaTime;

        float timeSinceLastSpawn = gameTimeElapsed - lastSpawnTime;
        if (timeSinceLastSpawn >= SpawnInterval)
        {
            spawnEnemies();
            lastSpawnTime = Time.time;
        }
    }

    private void determineMaxPresence()
    {
        // Determine the current max presence based on factors such as time and score
        int presenceBasedOnTime = Mathf.FloorToInt(gameTimeElapsed / 60f * MaxPresenceIncreasePerMinute);
        MaxPresence = DEFAULT_MAX_PRESENCE + presenceBasedOnTime;
    }

    private void spawnEnemies()
    {
        bool spawnLoopCapHit = true;
        for (int i = 0; i < SPAWN_LOOP_CAP; i++)
        {
            if(CurrentPresence >= MaxPresence)
            {
                spawnLoopCapHit = false;
                break;
            }

            spawnEnemy(Enemy);
        }

        if(spawnLoopCapHit)
        {
            Debug.LogWarning("Spawn loop cap was hit. This could be a bug.");
        }
    }

    private Enemy spawnEnemy(GameObject _enemy)
    {
        // Instantiate object
        GameObject newEnemy = GameObject.Instantiate(_enemy);
        newEnemy.transform.parent = enemyContainer;
        newEnemy.transform.position = getSpawnLocation();

        Enemy spawnedEnemy = newEnemy.GetComponent<Enemy>();
        spawnedEnemy.OnKilled += () =>
        {
            CurrentPresence -= spawnedEnemy.Presence;
        };

        // Set movement target
        EnemyMovement movement = newEnemy.GetComponent<EnemyMovement>();
        movement.Target = Player;
        
        CurrentPresence += spawnedEnemy.Presence;

        return spawnedEnemy;
    }

    private Vector3 getSpawnLocation()
    {
        float randomAngle = Random.value * 360f;
        Vector3 relativeSpawnVector = Quaternion.AngleAxis(randomAngle, Vector3.up) * Vector3.forward * SpawnRadius;
        Vector3 playerPosition = Player.transform.position;
        return playerPosition + relativeSpawnVector;
    }
}
