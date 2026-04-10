using UnityEngine;
using UnityEngine.InputSystem; 

public class TowerBuilder : MonoBehaviour
{
    public GameObject towerPrefab;
    public int towerCost = 100;
    private bool isBuilding = false;

    public void StartBuilding()
    {
        if (EconomyManager.Instance.money >= towerCost)
        {
            isBuilding = true;
            Debug.Log("Modo construcción activado. Haz clic en el suelo.");
        }
    }

    void Update()
    {
        
        if (isBuilding && Mouse.current.leftButton.wasPressedThisFrame)
        {
           
            Vector2 mousePosition = Mouse.current.position.ReadValue();
            Ray ray = Camera.main.ScreenPointToRay(mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (EconomyManager.Instance.SpendMoney(towerCost))
                {
                    Instantiate(towerPrefab, hit.point, Quaternion.identity);
                    isBuilding = false;
                }
            }
        }
    }
}