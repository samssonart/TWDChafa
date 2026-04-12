using UnityEngine;

public class Projectile : MonoBehaviour
{
    GameObject target;
    public float speed = 10f;
    int damage;

    public void Initialize(GameObject newTarget, int newDamage)
    {
        target = newTarget;
        damage = newDamage;
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, target.transform.position) < 0.2f)
        {
            if (target.TryGetComponent<Enemy>(out var enemy))
                enemy.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
