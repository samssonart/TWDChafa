using UnityEngine;
using UnityEngine.UI;

// Clase de UI que se comunica con el GameManager para comprar torres
public class GameUI : MonoBehaviour
{
    [Header("Tower Compras")]
    [SerializeField] private Button buyTowerButton;
    [SerializeField] private GameObject towerPrefab;
    [SerializeField] private Transform towerParent;
    [SerializeField] private int towerCost = 50;

    private void Start()
    {
        if (buyTowerButton != null)
        buyTowerButton.onClick.AddListener(OnBuyTowerClicked);
    }

    private void OnBuyTowerClicked()
    {

        if (towerPrefab == null || towerParent == null || GameManager.Instance == null)

        {
            Debug.LogWarning("Game UI No referencias para tower purachase");
            return;
        }
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