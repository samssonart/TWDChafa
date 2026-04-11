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

        if (fireTimer < 1f / fireRate) return;

        Enemy nearest = null;
        float nearestDist = float.MaxValue;

        foreach (Enemy e in GameManager.Instance.activeEnemies)
        {
            float d = Vector3.Distance(transform.position, e.transform.position);
            if (d < nearestDist && d <= range)
            {
                nearest = e;
                nearestDist = d;
            }
        }

        if (nearest != null)
        {
            GameObject p = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
            Projectile proj = p.GetComponent<Projectile>();

            proj.target = nearest.gameObject;

            fireTimer = 0f;
        }
    }
}