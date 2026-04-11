using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float speed;
    private int health;
    private int reward;

    private int currentWaypoint = 0;
    private Transform[] waypoints;

    public void Init(EnemyData data, Transform[] path)
    {
        speed = data.speed;
        health = data.health;
        reward = data.reward;
        waypoints = path;
    }

    void Update()
    {
        if (waypoints == null || waypoints.Length == 0) return;

        Vector3 target = waypoints[currentWaypoint].position;

        Vector3 lookAtTarget = new Vector3(target.x, transform.position.y, target.z);
        transform.LookAt(lookAtTarget);

        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target) < 0.1f)
        {
            currentWaypoint++;
            if (currentWaypoint >= waypoints.Length)
            {
                GameManager.Instance.LoseLife(1);
                Die();
            }
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            GameManager.Instance.AddMoney(reward);
            Die();
        }
    }

    private void Die()
    {
        GameManager.Instance.activeEnemies.Remove(this);
        Destroy(gameObject);
    }
}