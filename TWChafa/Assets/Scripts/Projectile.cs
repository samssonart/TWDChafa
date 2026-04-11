using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject target;
    public float speed = 10f;
    public int damage = 1;

    void OnEnable()
    {
        target = null;
    }

    void Update()
    {
        if (target == null || !target.activeInHierarchy)
        {
            ReturnPool();
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, target.transform.position) < 0.2f)
        {
            IDamageable damageable = target.GetComponent<IDamageable>();

            if (damageable != null )
            {
                damageable.TakeDamage(damage);
            }
            ReturnPool();
        }
    }

    void ReturnPool()
    {
        target = null;
        ProjectilePool.Instance.ReturnProjectile(gameObject);
    }
}