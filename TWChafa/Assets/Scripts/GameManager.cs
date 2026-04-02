using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

// Clase principal que maneja el dinero, vidas, spawn de enemigos y actualiza la UI
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int money = 100;
    public int lives = 10;
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI livesText;
    public GameObject[] enemyPrefabs;
    public Transform spawnPoint;

    private float spawnTimer = 0f;
    public float spawnInterval = 2f;

    void Awake()
    {
        // Singleton asegurado
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    void Start()
    {
        UpdateUI();
    }

    void Update()
    {
        spawnTimer += Time.deltaTime;
        // Separación lógica de spawn
        if (spawnTimer >= spawnInterval)
        {
            SpawnEnemy();
            spawnTimer = 0f;
        }

        //Mejor al UI para debug
        if (Keyboard.current != null && Keyboard.current.mKey.wasPressedThisFrame)
        {
            money += 10;
            UpdateUI();
        }
    }

    // Mejor organización para el SpawEnemy
    void SpawnEnemy()
    {
        if (enemyPrefabs == null || enemyPrefabs.Length == 0 || spawnPoint == null)
        {
            Debug.LogError("No hay enemigos asignados");
            return;
        }

        // Enemigos aleatorios
        GameObject enemyToSpawn = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

        Instantiate(enemyToSpawn, spawnPoint.position, Quaternion.identity);
    }

    public void AddMoney(int amount)
    {
        money += amount;
        UpdateUI();
    }

    public bool SpendMoney(int amount)
    {
        if (money >= amount)
        {
            money -= amount;
            UpdateUI();
            return true;
        }

        return false;
    }

    public void LoseLife(int amount)
    {
        lives -= amount;
        UpdateUI();

        if (lives <= 0)
        {
            GameOver();
        }
    }

    // Mejor organización para el GameOver
    void GameOver()
    {
        Debug.Log("Ya valiste.");
        Time.timeScale = 0f;
    }

    void UpdateUI()
    {
        if (moneyText != null)
            moneyText.text = "$ " + money;

        if (livesText != null)
            livesText.text = "Vidas: " + lives;
    }
}