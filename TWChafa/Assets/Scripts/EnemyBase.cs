using UnityEngine;

public abstract class EnemyBase : MonoBehaviour, IDamageable
{
    public float speed;
    public int health;
    public int reward = 5;
    public int damageToPlayer = 1;

    protected int currentWaypoint = 0;

    protected virtual void Update()
    {
        MoveAlongPath();
    }

    protected virtual void MoveAlongPath()
    {
        if (PathManager.Instance == null || PathManager.Instance.waypoints.Length == 0) return;

        Vector3 target = PathManager.Instance.waypoints[currentWaypoint].position;
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target) < 0.1f)
        {
            currentWaypoint++;
            if (currentWaypoint >= PathManager.Instance.waypoints.Length)
            {
                ReachGoal();
            }
        }
    }

    public virtual void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0) Die();
    }

    protected virtual void Die()
    {
        if (EconomyManager.Instance != null)
        {
            EconomyManager.Instance.AddMoney(reward);
        }
        Destroy(gameObject);
    }

    protected virtual void ReachGoal()
    {
       
        Destroy(gameObject);
    }
}