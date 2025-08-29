using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    public Enemy[] enemies;

    public Transform[] spawnPos;

    public float spawnInterval = 0.5f;

    private float spawnTime;
    private int spawnCount = 0;
    private int spawnMax = 20;

	private void Start()
	{
		spawnTime = Time.time;
	}

	private void Update()
	{
        if (spawnCount < spawnMax && Time.time - spawnTime > spawnInterval)
        {
            SpawnEnemy();
            spawnCount++;

            spawnTime = Time.time;
        }
	}

	public void SpawnEnemy()
    {
        var pos = spawnPos[Random.Range(0, spawnPos.Length)];
        var enemy = enemies[Random.Range(0, enemies.Length)];
        var spawned = Instantiate(enemy, pos.position, pos.rotation);

        spawned.OnDeath += () => spawnCount--;
    }
}
