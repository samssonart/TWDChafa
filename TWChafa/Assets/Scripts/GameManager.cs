using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int money = 100;
    public int lives = 10;

    [Header("Configuración de Enemigos")]
    public GameObject enemyPrefab;
    public Transform spawnPoint;
    public Transform[] pathWaypoints;

    public EnemyData normalEnemyData;
    public EnemyData tankEnemyData;

    private float spawnTimer = 0f;
    public float spawnInterval = 2f;
    private int enemyCounter = 0;

    public List<Enemy> activeEnemies = new List<Enemy>();

    public event Action<int> OnMoneyChanged;
    public event Action<int> OnLivesChanged;

    void Awake()
    {
        if (Instance != null) Destroy(Instance.gameObject);
        Instance = this;
    }

    void Start()
    {
        OnMoneyChanged?.Invoke(money);
        OnLivesChanged?.Invoke(lives);
    }

    void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnInterval)
        {
            SpawnEnemy();
            spawnTimer = 0f;
        }

        if (Keyboard.current.mKey.wasPressedThisFrame)
        {
            AddMoney(10);
        }
    }

    void SpawnEnemy()
    {
        GameObject newEnemyObj = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
        Enemy enemyScript = newEnemyObj.GetComponent<Enemy>();

        EnemyData dataToUse = (enemyCounter % 4 == 0) ? tankEnemyData : normalEnemyData;
        enemyCounter++;

        enemyScript.Init(dataToUse, pathWaypoints);

        activeEnemies.Add(enemyScript);
    }

    public void AddMoney(int amount)
    {
        money += amount;
        OnMoneyChanged?.Invoke(money);
    }

    public bool SpendMoney(int amount)
    {
        if (money >= amount)
        {
            money -= amount;
            OnMoneyChanged?.Invoke(money);
            return true;
        }
        return false;
    }

    public void LoseLife(int amount)
    {
        lives -= amount;
        OnLivesChanged?.Invoke(lives);
        if (lives <= 0)
        {
            Debug.Log("Ya valiste.");
            Time.timeScale = 0f;
        }
    }
}