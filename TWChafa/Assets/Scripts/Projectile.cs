using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject target;

    [SerializeField] private float speed = 10f;
    [SerializeField] private int damage = 1;

    private void Update()
    {
        MoveToTarget();
    }

    private void MoveToTarget()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        transform.position = Vector3.MoveTowards(
            transform.position,
            target.transform.position,
            speed * Time.deltaTime
        );

        float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);

        if (distanceToTarget <= 0.2f)
        {
            Enemy enemy = target.GetComponent<Enemy>();

            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            Destroy(gameObject);
        }
    }
}