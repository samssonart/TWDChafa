using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Transform target;
    public float speed = 10f;
    public int damage = 1;

    private IDamageable damageableTarget;

    public void SetTarget(Transform NuevoObjetivo)
    {
        target = NuevoObjetivo;

        if (target != null)
        {
            damageableTarget = target.GetComponent<IDamageable>();
        }
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Move();

        if (Vector3.Distance(transform.position, target.position) < 0.2f)
        {
            HitTarget();
        }
    }

    void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position,target.position,speed * Time.deltaTime);
    }

    void HitTarget()
    {
        if (damageableTarget != null)
        {
            damageableTarget.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}