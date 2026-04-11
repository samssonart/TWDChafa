using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Clase de UI que se comunica con el GameManager para comprar torres
public class GameUI : MonoBehaviour
{
    [Header("Referencias de UI")]
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI livesText;
    public Button buyTowerButton;

    [Header("Ajustes de Tienda")]
    public GameObject towerPrefab;
    public int towerCost = 50;

    void Start()
    {
        buyTowerButton.onClick.AddListener(OnBuyTowerClicked);
    }

    void Update()
    {
        if (GameManager.Instance != null)
        {
            moneyText.text = "$ " + GameManager.Instance.CurrentMoney;
            livesText.text = "Vidas: " + GameManager.Instance.CurrentLives;
        }
    }
    void OnBuyTowerClicked()
    {
        if (GameManager.Instance.SpendMoney(towerCost))
        {
            PlaceTower();
        }
        else
        {
            Debug.Log("No hay suficiente dinero para comprar la torre.");
        }
    }

    void PlaceTower()
    {
        Instantiate(towerPrefab, Vector3.zero, Quaternion.identity);
    }
}