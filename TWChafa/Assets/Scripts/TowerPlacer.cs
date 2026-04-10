using UnityEngine;

public class TowerPlacer : MonoBehaviour
{
    public GameObject towerPrefab;
    public LayerMask groundLayer;
    public int towerCost = 100;

    void Update()
    {
        if (!GameUI.isPlacingTower) return;

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, 100f, groundLayer))
            {
                if (GameManager.Instance.SpendMoney(towerCost))
                {
                    Vector3 pos = hit.point;
                    pos.y = 0.5f;
                    Instantiate(towerPrefab, pos, Quaternion.identity);
                    GameUI.isPlacingTower = false;
                }
            }
        }
    }
}