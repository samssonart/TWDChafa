using UnityEngine;

public class Tower : MonoBehaviour
{
    public float range = 5f;
    public float fireRate = 1f;
    public GameObject projectilePrefab;
    public Transform firePoint;

    private float fireTimer = 0f;

    void Update()
    {
        fireTimer += Time.deltaTime;

        // Mejora del Disparo
        if (fireTimer < 1f / fireRate) return;
        GameObject nearest = FindNearestEnemy();

        if (nearest != null)
        {
            Shoot(nearest);
            fireTimer = 0f;
        }
    }

    // Mejor organización para encontrar enemigos
    GameObject FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        GameObject nearest = null;
        float nearestDist = float.MaxValue;

        foreach (GameObject e in enemies)
        {
            float d = Vector3.Distance(transform.position, e.transform.position);

            if (d < nearestDist && d <= range)
            {
                nearest = e;
                nearestDist = d;
            }
        }

        return nearest;
    }

    // Nueva clase para manejar el disparo, con validaciones y mejor AutoAim
    void Shoot(GameObject target)
    {

        if (projectilePrefab == null || firePoint == null)
        {
            Debug.LogError("Faltan referencias en Tower (projectilePrefab o firePoint)");
            return;
        }

        GameObject p = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);

        // Validar que el prefab tenga el script Projectile
        Projectile proj = p.GetComponent<Projectile>();

        if (proj != null)
        {
            proj.target = target;
        }
        else
        {
            Debug.LogError("El prefab no tiene el script Projectile");
        }

        //Mejor AutoAim del Tower
        Vector3 direction = target.transform.position - transform.position;
        direction.y = 0;

        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }
}