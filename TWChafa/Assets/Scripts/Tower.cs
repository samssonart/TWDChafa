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

        Collider[] enemies=Physics.OverlapSphere(transform.position, range,LayerMask.GetMask("Enemy"));
        GameObject nearest = null;
        float nearestDist = float.MaxValue;

        foreach (Collider col in enemies)
        {
            float d = Vector3.Distance(transform.position, col.transform.position);
            if (d < nearestDist && d <= range)
            {
                nearest = col.gameObject;
                nearestDist = d;
            }
        }

        if (nearest != null)
        {
            GameObject p = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
            Projectile proj = p.GetComponent<Projectile>();
            proj.target = nearest;
            fireTimer = 0f;
        }
    }
}

