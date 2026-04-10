using UnityEngine;

public class WaypointManager : MonoBehaviour
{
    public static WaypointManager Instance;
    public Transform[] waypoints;

    void Awake()
    {
        Instance = this;
        waypoints = new Transform[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            waypoints[i] = transform.GetChild(i);
        }
    }
}