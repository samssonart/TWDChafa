using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float baseSpeed = 2f;
    private int currentWaypoint = 0;
    private Transform[] path; 

    
    float CurrentSpeed => baseSpeed + (Time.timeSinceLevelLoad * 0.01f);

    public void SetPath(Transform[] officialPath)
    {
        path = officialPath;
    }

    void Update()
    {
        if (path == null || currentWaypoint >= path.Length) return;

        Vector3 target = path[currentWaypoint].position;
        transform.position = Vector3.MoveTowards(transform.position, target, CurrentSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target) < 0.1f)
        {
            currentWaypoint++;
            if (currentWaypoint >= path.Length)
            {
                GameManager.Instance.LoseLife(1);
                Destroy(gameObject);
            }
        }
    }
}