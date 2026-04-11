using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

// Clase principal que maneja el dinero, vidas, spawn de enemigos y actualiza la UI
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Ajustes de Juego")]
    [SerializeField] private int money = 100;
    [SerializeField] private int lives = 10;

    [Header("Referencias de Spawning")]
    public GameObject enemyPrefab;
    public Transform spawnPoint;
    public float spawnInterval = 2f;
    private float spawnTimer = 0f;

    public WayPointRoute mainRoute;

    public int CurrentMoney => money;
    public int CurrentLives => lives;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void OnEnable()
    {
        Enemy.OnEnemyKilled += AddMoney;
        Enemy.OnEnemyReachedEnd += LoseLife;
    }

    void OnDisable()
    {
        Enemy.OnEnemyKilled -= AddMoney;
        Enemy.OnEnemyReachedEnd -= LoseLife;
    }

    void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnInterval)
        {
            SpawnEnemy();
            spawnTimer = 0f;
        }
    }

    void SpawnEnemy()
    {
        if (enemyPrefab == null || spawnPoint == null || mainRoute == null) return;

        GameObject enemyObj = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
        Enemy enemyScript = enemyObj.GetComponent<Enemy>();

        if (enemyScript != null)
        {
            enemyScript.Setup(mainRoute);
        }
    }

    public void AddMoney(int amount) => money += amount;

    public bool SpendMoney(int amount)
    {
        if (money >= amount) { money -= amount; return true; }
        return false;
    }

    public void LoseLife(int amount)
    {
        lives -= amount;
        if (lives <= 0) { Time.timeScale = 0f; Debug.Log("Game Over");}
    }
}