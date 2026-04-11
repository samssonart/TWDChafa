using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public EnemyFactory factory;
    public Transform spawnPoint;
    public Transform[] waypoints;

    public float spawnInterval = 2f;
    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnEnemy();
            timer = 0f;
        }
    }

    void SpawnEnemy()
    {
        EnemyType type = GetRandomEnemyType();

        Enemy enemy = factory.CreateEnemy(type, spawnPoint.position);
        enemy.Initialize(waypoints);

        // aca conectas los eventos para el enemigo
        enemy.Muerto += HandleEnemyDeath;
        enemy.LlegarFinal += HandleEnemyReachedEnd;
    }

    EnemyType GetRandomEnemyType()
    {
        int rand = Random.Range(0, 3);

        switch (rand)
        {
            case 1: return EnemyType.Fast;
            case 2: return EnemyType.Tank;
            default: return EnemyType.Normal;
        }
    }

    void HandleEnemyDeath(Enemy enemy)
    {
        GameManager.Instance.AddMoney(enemy.reward);
    }

    void HandleEnemyReachedEnd(Enemy enemy)
    {
        GameManager.Instance.LoseLife(1);
    }
}