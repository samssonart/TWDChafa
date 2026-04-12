using UnityEngine;
using System.Linq;

public class Enemy : MonoBehaviour
{
    [HideInInspector] public EnemyData data;
    [HideInInspector] public int currentHealth;
    private int currentWaypoint = 0;
    private Transform[] waypoints;

    public void Initialize(EnemyData enemyData)
    {
        data = enemyData;
        currentHealth = data.health;
        tag = "Enemy";
        
        waypoints = Object.FindObjectsOfType<Waypoint>()
            .Select(w => w.transform)
            .OrderBy(t => t.name)  
            .ToArray();

        Debug.Log($"Enemy '{data.enemyName}' spawned. Found {waypoints.Length} waypoints.", gameObject);
        if (waypoints.Length == 0) Debug.LogError("No Waypoints", this);
    }

    void Update()
    {
        
        if (waypoints == null || waypoints.Length == 0)
        {
            Debug.LogWarning("Enemy sin waypoints, destruyendo...", gameObject);
            Destroy(gameObject);
            return;
        }

        Transform target = waypoints[Mathf.Clamp(currentWaypoint, 0, waypoints.Length - 1)];
        transform.position = Vector3.MoveTowards(transform.position, target.position, data.speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            currentWaypoint++;
            if (currentWaypoint >= waypoints.Length)
            {
                GameManager.Instance.LoseLife(1);
                Destroy(gameObject);
            }
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            GameManager.Instance.AddMoney(data.reward);
            Destroy(gameObject);
        }
    }
}
