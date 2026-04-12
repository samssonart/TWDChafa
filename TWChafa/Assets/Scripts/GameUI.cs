using UnityEngine;
using UnityEngine.UI;

// Clase de UI que se comunica con el GameManager para comprar torres
public class GameUI : MonoBehaviour
{
    public Button buyTowerButton;
    public GameObject towerPrefab;
    public Transform towerParent;
    public int towerCost = 100;
    void Start()
    {
        buyTowerButton.onClick.AddListener(OnBuyTowerClicked);
    }

    void OnBuyTowerClicked()
    {
        if (!GameManager.Instance.SpendMoney(towerCost))
        {
            Debug.Log("No hay dinero");
            return;
        }

        GameObject[] areas = GameObject.FindGameObjectsWithTag("BuildArea");

        foreach (GameObject area in areas)
        {

            if (area.transform.childCount > 0)
                continue;

            GameObject t = Instantiate(
                towerPrefab,
                area.transform.position + Vector3.up * 1.79f,
                Quaternion.identity
            );

            t.transform.SetParent(area.transform);

            Debug.Log("TORRE COLOCADA EN AREA");
            return;
        }

        Debug.Log("No hay espacios libres");
    }
}