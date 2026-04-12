using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [Header("Botón de compra")]
    [SerializeField] private Button buyTowerButton;

    [Header("Torre")]
    [SerializeField] private GameObject towerPrefab;
    [SerializeField] private Transform towerParent;
    [SerializeField] private int towerCost = 50;

    private void Start()
    {
        if (buyTowerButton != null)
        {
            buyTowerButton.onClick.AddListener(OnBuyTowerClicked);
        }
    }

    private void OnBuyTowerClicked()
    {
        if (GameManager.Instance == null)
        {
            return;
        }

        bool couldBuyTower = GameManager.Instance.SpendMoney(towerCost);

        if (!couldBuyTower)
        {
            Debug.Log("No hay suficiente dinero para comprar la torre.");
            return;
        }

        Instantiate(towerPrefab, towerParent.position, Quaternion.identity, towerParent);

        GameManager.Instance.RegisterTowerPurchase();
    }
}