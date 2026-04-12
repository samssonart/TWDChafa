using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Movimiento")]
    [SerializeField] protected float speed = 2f;

    [Header("Stats")]
    [SerializeField] protected int health = 10;
    [SerializeField] protected int reward = 5;
    [SerializeField] protected int damageToBase = 1;

    protected Transform[] waypoints;
    protected int currentWaypointIndex = 0;

    private void Update()
    {
        MoveAlongPath();
    }

    public void SetWaypoints(Transform[] newWaypoints)
    {
        waypoints = newWaypoints;
        currentWaypointIndex = 0;
    }

    private void MoveAlongPath()
    {
        if (waypoints == null || waypoints.Length == 0)
        {
            return;
        }

        if (currentWaypointIndex >= waypoints.Length)
        {
            ReachBase();
            return;
        }

        Transform targetWaypoint = waypoints[currentWaypointIndex];

        transform.position = Vector3.MoveTowards(
            transform.position,
            targetWaypoint.position,
            speed * Time.deltaTime
        );

        float distanceToWaypoint = Vector3.Distance(transform.position, targetWaypoint.position);

        if (distanceToWaypoint <= 0.05f)
        {
            currentWaypointIndex++;

            if (currentWaypointIndex >= waypoints.Length)
            {
                ReachBase();
            }
        }
    }

    protected void ReachBase()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.LoseLife(damageToBase);
        }

        Destroy(gameObject);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    protected void Die()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.AddMoney(reward);
        }

        Destroy(gameObject);
    }
}