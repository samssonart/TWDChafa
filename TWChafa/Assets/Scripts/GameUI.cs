using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;


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
          
           Instantiate(towerPrefab, towerParent.position, Quaternion.identity);
        }
        else
        {
            Debug.Log("No hay suficiente dinero para comprar la torre.");
        }
    }
}