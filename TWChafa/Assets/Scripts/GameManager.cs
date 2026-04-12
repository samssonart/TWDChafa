using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Stats")]
    public int money = 100;
    public int lives = 10;

    [Header("UI")]
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI livesText;

    [Header("Spawning")]
    public Transform spawnPoint;
    public List<EnemyData> enemyDatas = new(); // Asigna Basic y Tank en Inspector
    private float spawnTimer = 0f;
    public float spawnInterval = 2f;

    public System.Action<int> OnMoneyChanged;
    public System.Action<int> OnLivesChanged;

    void Awake()
    {
        if (Instance != null) Destroy(Instance.gameObject);
        Instance = this;
    }

    void Start() => UpdateUI();

    void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnInterval)
        {
            SpawnRandomEnemy();
            spawnTimer = 0f;
        }
        if (Keyboard.current.mKey.wasPressedThisFrame)
        {
            money += 10;
            UpdateUI();
            OnMoneyChanged?.Invoke(money);
        }
    }

    void SpawnRandomEnemy()
    {
        // CHECK 1: EnemyDatas
        if (enemyDatas == null || enemyDatas.Count == 0)
        {
            Debug.LogError("GameManager: ¡AGREGA EnemyData(s)! Size=0");
            return;
        }

        EnemyData data = enemyDatas[Random.Range(0, enemyDatas.Count)];
        if (data?.prefab == null)
        {
            Debug.LogError($"EnemyData '{data?.enemyName}' → PREFAB NULL! Arrastra EnemyPrefab al SO");
            return;
        }

        // CHECK 2: SpawnPoint (¡ESTE ES EL CULPABLE!)
        if (spawnPoint == null)
        {
            Debug.LogError("GameManager: SPAWNPOINT NULL! Crea Empty 'SpawnPoint' en Hierarchy y arrástralo.");
            return;
        }

        Debug.Log($"✅ Spawneando {data.enemyName} en {spawnPoint.position}");

        GameObject enemyObj = Instantiate(data.prefab, spawnPoint.position, Quaternion.identity);
        Enemy enemyScript = enemyObj.GetComponent<Enemy>();
        if (enemyScript != null)
            enemyScript.Initialize(data);
        else
            Debug.LogError("EnemyPrefab SIN Enemy.cs component!");
    }
    public bool SpendMoney(int amount)
    {
        if (money >= amount)
        {
            money -= amount;
            UpdateUI();
            OnMoneyChanged?.Invoke(money);
            return true;
        }
        return false;
    }

    public void AddMoney(int amount)
    {
        money += amount;
        UpdateUI();
        OnMoneyChanged?.Invoke(money);
    }

    public void LoseLife(int amount)
    {
        lives -= amount;
        UpdateUI();
        OnLivesChanged?.Invoke(lives);
        if (lives <= 0) Time.timeScale = 0f;
    }

    void UpdateUI()
    {
        moneyText.text = "$" + money;
        livesText.text = "Vidas: " + lives;
    }
}
