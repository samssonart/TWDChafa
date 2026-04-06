using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject target;
    public float speed = 10f;
    public int damage = 1;

    void Update()
    {
        // Validar Target
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        // AutoAim
        transform.position = Vector3.MoveTowards(
            transform.position,
            target.transform.position,
            speed * Time.deltaTime
        );

        // Distancia de Impacto
        if (Vector3.Distance(transform.position, target.transform.position) < 0.2f)
        {
            IDamageable damageable = target.GetComponent<IDamageable>();

            if (damageable != null)
            {
                damageable.TakeDamage(damage);
            }
            else
            {
                // Evita crash y ayuda a debug
                Debug.LogWarning("El objeto no implementa IDamageable");
            }
            Destroy(gameObject);
        }
    }
}