using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

// Clase principal que maneja el dinero, vidas, spawn de enemigos y actualiza la UI
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Data")]
    public int _money = 20;
    public int _lives = 10;

    [Header("Text")]
    public TextMeshProUGUI _moneyText;
    public TextMeshProUGUI _livesText;
    
    public Transform _spawnPoint;

    public float _spawnInterval = 2f;

    public EnemyFactory _enemyFactory;

    private float spawnTimer = 0f;
    

    void Awake()
    {
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

        if (spawnTimer >= _spawnInterval)
        {
            SpawnEnemy();
            spawnTimer = 0f;
        }
        // Te da dinero para probar, espero que los jugadores no sean tramposos
        if (Keyboard.current != null && Keyboard.current.mKey.wasPressedThisFrame)
        {
            _money += 10;
            UpdateUI();
        }
    }

    void SpawnEnemy()
    {
        if (_enemyFactory == null || _spawnPoint == null)
        {
            return;
        }
        Variations spawn = Random.value > 0.7f ? Variations.Fast : Variations.Normal;
        _enemyFactory.CreateEnemy(spawn, _spawnPoint.position);
    }

    public void AddMoney(int amount)
    {
        _money += amount;
        UpdateUI();
    }

    public bool SpendMoney(int amount)
    {
        if (_money >= amount)
        {
            _money -= amount;
            UpdateUI();
            return true;
        }

        return false;
    }

    public void LoseLife(int amount)
    {
        _lives -= amount;
        UpdateUI();

        if (_lives <= 0)
        {
            // Estas muerto
            Debug.Log("Ya valiste.");
            Time.timeScale = 0f;
        }
    }

    void UpdateUI()
    {
        if (_moneyText != null)
        {
            _moneyText.text = "$ " + _money;
        }

        if (_livesText != null)
        {
            _livesText.text = "Vidas: " + _lives;
        }
    }
}