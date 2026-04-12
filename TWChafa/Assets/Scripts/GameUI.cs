using UnityEngine;
using UnityEngine.UI;

// Clase de UI que se comunica con el GameManager para comprar torres
public class GameUI : MonoBehaviour
{
    public Button _buyTowerButton;
    public GameObject _towerPrefab;
    public Transform _towerParent;
    public int _towerCost = 50;

    public Transform[] _towerSpawn;
    private bool[] _usedSpawns;

    void Start()
    {
        if (_buyTowerButton != null)
        {
            _buyTowerButton.onClick.AddListener(OnBuyTowerClicked);
        }
        if (_towerSpawn != null && _towerSpawn.Length > 0)
        {
            _usedSpawns = new bool[_towerSpawn.Length];

        }
    }

    void OnBuyTowerClicked()
    {
        if (_towerPrefab == null || _towerParent == null || _towerSpawn == null || _towerSpawn.Length == 0)
        {
            Debug.Log("Faltan referencias");
            return;
        }

        if (GameManager.Instance == null)
        {
            Debug.Log("No existe GameManager.");
            return;
        }

        int index = SpawnIndex();

        if (index == -1)
        {
            Debug.Log("No hayu espacios Disponibles");
            return;
        }

        bool canBuy = GameManager.Instance.SpendMoney(_towerCost);

        if (canBuy)
        {
            Transform _spawnPoint = _towerSpawn[index];
            GameObject s = Instantiate(_towerPrefab, _spawnPoint.position, _spawnPoint.rotation);
            s.transform.SetParent(_towerParent);

            _usedSpawns[index] = true;
        }
        else
        {
            Debug.Log("No hay suficiente dinero para comprar la torre.");
        }
    }
    int SpawnIndex()
    {
        for (int i = 0; i < _usedSpawns.Length; i++)
        {
            if (!_usedSpawns[i])
            {
                return i;
            }
        }

        return -1;
    }
}