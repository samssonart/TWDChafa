using TMPro;
using UnityEngine;


// Clase principal que maneja el dinero, vidas, spawn de enemigos y actualiza la UI
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Game Start")]
    [SerializeField] private int startingMoney = 100;
    [SerializeField] private int startingLives = 10;
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private float goldenSpawnInterval = 10f;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private TextMeshProUGUI livesText;

    [Header("Enemy Spawn")]
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject goldenEnemyPrefab;
    

  
    public int Money { get; private set; }
    public int Lives { get; private set; }


    private float spawnTimer;
    private float goldenSpawnTimer;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;

            
        }

        Instance = this;


    }
   

    void Start()
    {
       Money = startingMoney;
        Lives = startingLives;
        UpdateUI();
    }

    void Update()
    {
        HandleEnemySpawn();
      
    }

    public void  HandleEnemySpawn()
    {
        if (enemyPrefab == null || spawnPoint == null)

        {
            return;
        }

        spawnTimer += Time.deltaTime;
        goldenSpawnTimer += Time.deltaTime;


        if (spawnTimer >= spawnInterval)
        {
            SpawnEnemy();
            spawnTimer = 0;
        }

        if(goldenEnemyPrefab != null && goldenSpawnTimer >= goldenSpawnInterval)
        {
            SpawnGoldenEnemy();
            goldenSpawnTimer = 0;
        }



    }

    private void SpawnGoldenEnemy()
    {
        Instantiate(goldenEnemyPrefab, spawnPoint.position , Quaternion.identity);
    }

   private  void SpawnEnemy()
    {
        Instantiate(enemyPrefab, spawnPoint.position , Quaternion.identity);
    }

    public void AddMoney(int amount)
    {
       if (amount < 0)
        {
            return;
        }

        Money += amount;
        UpdateUI();
    }

    public bool SpendMoney(int amount)
    {
       if (amount < 0)
        {  return false; }

       if (Money < amount)
        {
            return false;
        }

       Money -= amount; 
        UpdateUI();
        return true;
    }

    public void LoseLife(int amount)
    {
        if (amount <= 0) { return; }

        Lives -= amount;

        if(Lives < 0)
        {
            Lives = 0;
        }

        UpdateUI();
        if (Lives == 0)
        {
            GameOver();
        }
            
    
    }

    private void GameOver()
    {
        Debug.Log("Game Over ");
        Time.timeScale = 0f;
    }

     private  void UpdateUI()
    {
        if ( moneyText != null )
        {
            moneyText.text = $"$ { Money} ";
        }

        if ( livesText != null )
        {
            livesText.text = $"Vidas :{Lives}";
        }
        
    }
}