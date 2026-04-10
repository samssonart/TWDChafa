using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic; 

public class GameUI : MonoBehaviour
{
    public Button buyTowerButton;
    public GameObject towerPrefab;
    public int towerCost = 100;

    void Start()
    {
        buyTowerButton.onClick.AddListener(OnBuyTowerClicked);
    }

    void OnBuyTowerClicked()
    {
        
        GameObject[] buildPoints = GameObject.FindGameObjectsWithTag("BuildPoint");
        List<GameObject> puntosDisponibles = new List<GameObject>();

        
        foreach (GameObject punto in buildPoints)
        {
            
            if (punto.transform.childCount == 0)
            {
                puntosDisponibles.Add(punto);
            }
        }

        
        if (puntosDisponibles.Count > 0)
        {
            if (GameManager.Instance.SpendMoney(towerCost))
            {
                
                GameObject puntoElegido = puntosDisponibles[0];

                
                GameObject t = Instantiate(towerPrefab, puntoElegido.transform.position, Quaternion.identity);
                t.transform.SetParent(puntoElegido.transform);
            }
            else
            {
                Debug.Log("No tienes dinero suficiente.");
            }
        }
        else
        {
            Debug.Log("¡No quedan espacios para más torres!");
        }
    }
}