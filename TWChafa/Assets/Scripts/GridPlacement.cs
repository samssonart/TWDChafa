using UnityEngine;

public class GridPlacement : MonoBehaviour
{
    public static GridPlacement Instance;
    public float gridSize = 1f;

    void Awake()
    {
        if (Instance != null) Destroy(Instance.gameObject);
        Instance = this;
    }

    public static Vector3 GetMouseGridPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Asume main cam
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, LayerMask.GetMask("Ground")))
        {
            Vector3 pos = hit.point;
            pos.x = Mathf.Round(pos.x / Instance.gridSize) * Instance.gridSize;
            pos.z = Mathf.Round(pos.z / Instance.gridSize) * Instance.gridSize;
            return pos;
        }
        return Vector3.zero;
    }
}
