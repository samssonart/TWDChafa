using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 2f;
    public int health = 10;
    public int reward = 5;

    public static event Action<int> OnEnemyReachedEnd;
    public static event Action<int> OnEnemyKilled;

    private int currentWaypointIndex = 0;
    private WayPointRoute route;

    public void Setup(WayPointRoute assignedRoute)
    {
        route = assignedRoute;
    }

    void Update()
    {
        if (route == null || currentWaypointIndex >= route.points.Length) return;

        Transform target = route.points[currentWaypointIndex];
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            currentWaypointIndex++;

            if (currentWaypointIndex >= route.points.Length)
            {
                OnEnemyReachedEnd?.Invoke(1);
                Destroy(gameObject);
            }
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            OnEnemyKilled?.Invoke(reward);
            Destroy(gameObject);
        }
    }
}
