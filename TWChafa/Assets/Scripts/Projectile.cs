using UnityEngine;

public class Projectile : MonoBehaviour
{
   
    public Transform target;
    public float speed = 10f;
    public int damage = 1;

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target.position) < 0.2f)
        {
        
            IDamageable damageable = target.GetComponent<IDamageable>();
            damageable?.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}