using UnityEngine;

public class TowerCombat : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float fireRate = 1f;
    public float range = 5f;

    private float timer;
    private Enemy currentTarget;

    void Update()
    {
        timer += Time.deltaTime;

        if (currentTarget == null)
        {
            FindTarget();
        }

        if (currentTarget != null && timer >= fireRate)
        {
            Shoot();
            timer = 0f;
        }
    }

    void FindTarget()
    {
        Enemy[] enemies = Object.FindObjectsByType<Enemy>(FindObjectsSortMode.None);

        float minDist = Mathf.Infinity;

        foreach (Enemy e in enemies)
        {
            float dist = Vector3.Distance(transform.position, e.transform.position);

            if (dist < range && dist < minDist)
            {
                minDist = dist;
                currentTarget = e;
            }
        }
    }

    void Shoot()
    {
        if (currentTarget == null) return;

        GameObject proj = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);

        Projectile p = proj.GetComponent<Projectile>();
        p.SetTarget(currentTarget.transform);
    }
}

