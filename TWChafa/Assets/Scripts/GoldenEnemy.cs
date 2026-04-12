using UnityEngine;

public class GoldenEnemy : MonoBehaviour
{
    [Header("Stat")]
    [SerializeField] private float moveSpeed = 1;
    [SerializeField] private int maxHealth = 20;
    [SerializeField] private int reward = 30;
    [SerializeField] private int lifeDamage = 1;


    private int currentHealth;
    private int currentWaypointIndex;
    private GameObject[] waypoints;

    private void Start()
    {
        currentHealth = maxHealth;
        waypoints = GameObject.FindGameObjectsWithTag("Waypoint");

        if (waypoints == null || waypoints.Length == 0)
        {
            Debug.LogWarning("GoldenEnemy : No waypoints encontrados.");
        }

    }

    private void Update()
    {
        if (waypoints == null || waypoints.Length == 0)
        {
            return;
        }

        if (currentWaypointIndex >= waypoints.Length)
        {
            ReachGoal();
            return;
        }

        MoveToNextWaypoint();
    }

    public void MoveToNextWaypoint()
    {
        Transform targetWaypoint = waypoints[currentWaypointIndex].transform;
        Vector3 targetPosition = targetWaypoint.position;

        transform.position = Vector3.MoveTowards(

            transform.position,
        targetPosition,
            moveSpeed * Time.deltaTime
            );

        if (Vector3.Distance(transform.position, targetPosition) <= 0.01f)
        {
            currentWaypointIndex++;
        }
    }

    public void TakeDamage(int damage)
    {
        if (damage <= 0)
        {
            return;
        }

        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }


    private void Die()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.AddMoney(reward);
        }

        Destroy(gameObject);
    }

    private void ReachGoal()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.LoseLife(lifeDamage);
        }

        Destroy(gameObject);
    }
}



