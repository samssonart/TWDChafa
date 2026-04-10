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
            Destroy(gameObject);
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, target.transform.position) < 0.2f)
        {
            Health health = target.GetComponent<Health>();
            if (health != null) health.TakeDamage(damage);
        }
    }
}