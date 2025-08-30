using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    public Enemy[] enemies;

    public Transform[] spawnPos;

    public UIManager manager;
    private GameManager gameManager;

    public float spawnInterval = 0.5f;

    private float spawnTime;
    private int spawnCount = 0;
    private int spawnMax = 20;

	private void Start()
	{
		spawnTime = Time.time;
		gameManager = GameObject.FindWithTag(Defines.gameManagerStr).GetComponent<GameManager>();
	}

	private void Update()
	{
        if (manager.IsPaused || gameManager.IsGameOver) return;

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
