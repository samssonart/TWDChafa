using UnityEngine;
using UnityEngine.InputSystem;

public class Tower : MonoBehaviour
{
    public static Tower Instance;

    public GameObject towerPrefab;
    public Transform towerParent;
    public int towerCost = 50;

    void Awake()
    {
        Instance = this;
    }

    public void TryBuyTower()
    {
        if (GameManager.Instance.SpendMoney(towerCost))
        {
            SpawnTower();
        }
        else
        {
            Debug.Log("No hay suficiente dinero.");
        }
    }

    private void SpawnTower()
    {
        Vector3 spawnPosition = transform.position;

        GameObject newTower = Instantiate(towerPrefab, spawnPosition, Quaternion.identity, towerParent);

        Debug.Log("Torre creada en: " + spawnPosition);
    }

}
