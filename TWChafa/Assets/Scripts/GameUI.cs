using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUI : MonoBehaviour
{
    public Button buyTowerButton;
    public GameObject towerPrefab;
    public Transform towerParent;
    public int towerCost = 50;

    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI livesText;

    void Start()
    {
        buyTowerButton.onClick.AddListener(OnBuyTowerClicked);

        GameManager.Instance.OnMoneyChanged += UpdateMoneyText;
        GameManager.Instance.OnLivesChanged += UpdateLivesText;

        UpdateMoneyText(GameManager.Instance.money);
        UpdateLivesText(GameManager.Instance.lives);
    }

    void OnDestroy()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnMoneyChanged -= UpdateMoneyText;
            GameManager.Instance.OnLivesChanged -= UpdateLivesText;
        }
    }

    private void UpdateMoneyText(int newAmount)
    {
        moneyText.text = "$ " + newAmount;
    }

    private void UpdateLivesText(int newLives)
    {
        livesText.text = "Vidas: " + newLives;
    }

    void OnBuyTowerClicked()
    {
        if (GameManager.Instance.SpendMoney(towerCost))
        {
            GameObject t = Instantiate(towerPrefab, Vector3.zero, Quaternion.identity);
            t.transform.SetParent(towerParent);
            t.transform.localPosition = Vector3.zero;
        }
        else
        {
            Debug.Log("No hay suficiente dinero para comprar la torre.");
        }
    }
}