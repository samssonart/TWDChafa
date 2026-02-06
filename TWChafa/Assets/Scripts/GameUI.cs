using UnityEngine;
using UnityEngine.UI;

// Clase de UI que se comunica con el GameManager para comprar torres
public class GameUI : MonoBehaviour
{
    public Button buyTowerButton;
    public GameObject towerPrefab;
    public Transform towerParent;
    public int towerCost = 50;

    void Start()
    {
        buyTowerButton.onClick.AddListener(OnBuyTowerClicked);
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