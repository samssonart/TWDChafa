using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

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
    public List<GameObject> enemies = new List<GameObject>();

    private float spawnTimer = 0f;
    public float spawnInterval = 2f;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this; 
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
            
        
      
       

    }

    void Start()
    {
        UpdateUI();
    }

    void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnInterval)
        {
            SpawnEnemy();
            spawnTimer = 0f;
        }
        // Te da dinero para probar, espero que los jugadores no sean tramposos
        if (Keyboard.current.mKey.wasPressedThisFrame)
        {
            money += 10;
            UpdateUI();
        }
    }

    void SpawnEnemy()
    {
       if (enemyPrefabs == null || enemyPrefabs.Length == 0) return;

       int randomIndex = UnityEngine.Random.Range(0, enemyPrefabs.Length);

       Instantiate(enemyPrefabs[randomIndex], spawnPoint.position, Quaternion.identity);
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
            // Estas muerto
            Debug.Log("Ya valiste.");
            Time.timeScale = 0f;
        }
    }

    void UpdateUI()
    {
        moneyText.text = "$ " + money;
        livesText.text = "Vidas: " + lives;
    }
}