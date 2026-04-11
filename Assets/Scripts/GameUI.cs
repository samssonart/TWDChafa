using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public Button buyTowerButton;

    void Start()
    {
        buyTowerButton.onClick.AddListener(OnBuyTowerClicked);
    }

    void OnBuyTowerClicked()
    {
        Tower.Instance.TryBuyTower();
    }
}