using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject target;
    public float speed = 10f;
    public int damage = 1;

    void Update()
    {
        if (target == null)
        {
            ProjectilePool.Instance.ReturnProjectile(gameObject);
            return;
        }

        Vector3 direction = target.transform.position - transform.position;

        transform.right = direction;

        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target.transform.position) < 0.2f)
        {
            Enemy enemy = target.GetComponent<Enemy>();
            enemy.TakeDamage(damage);

            ProjectilePool.Instance.ReturnProjectile(gameObject);
        }
    }
}