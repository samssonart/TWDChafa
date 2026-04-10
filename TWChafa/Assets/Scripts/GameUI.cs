using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public Button buyTowerButton;
    public int towerCost = 100;

    public static bool isPlacingTower = false;

    void Start()
    {
        buyTowerButton.onClick.AddListener(OnBuyTowerClicked);
    }

    void OnBuyTowerClicked()
    {
        if (GameManager.Instance.money >= towerCost)
        {
            isPlacingTower = true;
        }
        else
        {
            Debug.Log("No hay suficiente dinero para comprar la torre.");
        }
    }
}