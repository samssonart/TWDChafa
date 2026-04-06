using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public Button buyTowerButton;
    public GameObject towerPrefab;
    public Transform towerParent;
    public int towerCost = 50;
    public Transform[] towerSpawnPoints; // Nuevo para el GetSpawnPosition
    private int currentSpawnIndex = 0;

    void Start()
    {
        if (buyTowerButton != null)
        {
            buyTowerButton.onClick.AddListener(OnBuyTowerClicked);
        }
        else
        {
            Debug.LogError("buyTowerButton no está asignado en GameUI");
        }
    }

    void OnBuyTowerClicked()
    {
        if (GameManager.Instance == null)
        {
            Debug.LogError("GameManager no existe en la escena");
            return;
        }

        if (GameManager.Instance.SpendMoney(towerCost))
        {
            // Spawnea aleatoriamente en los puntos de spawn definidos
            Vector3 spawnPos = GetSpawnPosition();

            GameObject t = Instantiate(towerPrefab, spawnPos, Quaternion.identity);

            // Valida el towerParent para evitar errores
            if (towerParent != null)
            {
                t.transform.SetParent(towerParent);
            }
        }
        else
        {
            Debug.Log("No hay suficiente dinero para comprar la torre.");
        }
    }

    // Mejor colleción del SpawPosition
    Vector3 GetSpawnPosition()
    {
        if (towerSpawnPoints == null || towerSpawnPoints.Length == 0)
        {
            Debug.LogError("No hay puntos de spawn asignados");
            return Vector3.zero;
        }

        Transform spawnPoint = towerSpawnPoints[currentSpawnIndex];

        // Avanza al siguiente
        currentSpawnIndex++;
        return spawnPoint.position;
    }
}