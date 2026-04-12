using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Recursos del jugador")]
    [SerializeField] private int money = 100;
    [SerializeField] private int lives = 10;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private TextMeshProUGUI livesText;

    [Header("Spawn de enemigos")]
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject fastEnemyPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float spawnInterval = 2f;

    [Header("Ruta de enemigos")]
    [SerializeField] private Transform[] waypoints;

    [Header("Torres")]
    [SerializeField] private int towersBought = 0;

    private float spawnTimer;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        UpdateUI();
    }

    private void Update()
    {
        HandleEnemySpawn();
    }

    private void HandleEnemySpawn()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnInterval)
        {
            SpawnEnemy();
            spawnTimer = 0f;
        }
    }

    private void SpawnEnemy()
    {
        GameObject enemyToSpawn = enemyPrefab;

        if (towersBought >= 2 && fastEnemyPrefab != null)
        {
            int randomEnemy = Random.Range(0, 2);

            if (randomEnemy == 1)
            {
                enemyToSpawn = fastEnemyPrefab;
            }
        }

        GameObject newEnemyObject = Instantiate(enemyToSpawn, spawnPoint.position, Quaternion.identity);

        Enemy newEnemy = newEnemyObject.GetComponent<Enemy>();

        if (newEnemy != null)
        {
            newEnemy.SetWaypoints(waypoints);
        }
    }

    public void RegisterTowerPurchase()
    {
        towersBought++;
    }

    public int GetTowerCount()
    {
        return towersBought;
    }

    public void AddMoney(int amount)
    {
        money += amount;
        UpdateUI();
    }

    public bool SpendMoney(int amount)
    {
        if (money < amount)
        {
            return false;
        }

        money -= amount;
        UpdateUI();
        return true;
    }

    public void LoseLife(int amount)
    {
        lives -= amount;

        if (lives < 0)
        {
            lives = 0;
        }

        UpdateUI();

        if (lives == 0)
        {
            Debug.Log("Game Over");
            Time.timeScale = 0f;
        }
    }

    private void UpdateUI()
    {
        if (moneyText != null)
        {
            moneyText.text = "$ " + money;
        }

        if (livesText != null)
        {
            livesText.text = "Vidas: " + lives;
        }
    }
}