using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Estadísticas del Jugador")]
    public int money = 100;
    public int lives = 10;
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI livesText;

    [Header("Configuración de Enemigos")]
    public GameObject[] enemyPrefabs;
    public Transform spawnPoint;
    public Transform[] waypoints;

    [Header("Dificultad Progresiva (Innovación)")]
    public float spawnInterval = 2f;
    public float minSpawnInterval = 0.5f;
    public float decreaseRate = 0.02f;
    private float spawnTimer = 0f;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start() => UpdateUI();

    void Update()
    {
        
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnInterval)
        {
            SpawnEnemy();
            spawnTimer = 0f;
        }

        
        if (spawnInterval > minSpawnInterval)
        {
            spawnInterval -= decreaseRate * Time.deltaTime;
        }
    }

    void SpawnEnemy()
    {
        if (enemyPrefabs.Length == 0) return;

        int index = Random.Range(0, enemyPrefabs.Length);
        GameObject go = Instantiate(enemyPrefabs[index], spawnPoint.position, Quaternion.identity);

        
        go.GetComponent<Enemy>().SetPath(waypoints);
    }

    public void AddMoney(int amount) { money += amount; UpdateUI(); }

    public bool SpendMoney(int amount)
    {
        if (money >= amount) { money -= amount; UpdateUI(); return true; }
        return false;
    }

    public void LoseLife(int amount)
    {
        lives -= amount;
        UpdateUI();
        if (lives <= 0)
        {
            Debug.Log("HAS PERDIDO EL JUEGO");
            Time.timeScale = 0f;
        }
    }

    void UpdateUI()
    {
        moneyText.text = "$ " + money;
        livesText.text = "Vidas: " + lives;
    }
}