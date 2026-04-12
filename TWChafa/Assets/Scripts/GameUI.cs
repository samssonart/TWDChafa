using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public Button buyTowerButton;
    public GameObject towerPrefab;
    public int towerCost = 50;

    void Start()
    {
        buyTowerButton.onClick.AddListener(TryPlaceTower);
        GameManager.Instance.OnMoneyChanged += UpdateButtonInteractable;
    }

    void TryPlaceTower()
    {
        Vector3 gridPos = GridPlacement.GetMouseGridPosition(); // Snap a grid
        if (GameManager.Instance.SpendMoney(towerCost))
        {
            GameObject tower = Instantiate(towerPrefab, gridPos, Quaternion.identity);
        }
    }

    void UpdateButtonInteractable(int currentMoney) => buyTowerButton.interactable = currentMoney >= towerCost;
}
