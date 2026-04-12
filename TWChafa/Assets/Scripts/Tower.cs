using UnityEngine;

public class Tower : MonoBehaviour
{
    [Header("Attack Settigs")]
    [SerializeField]private float range = 5f;
    [SerializeField]private float fireRate = 1f;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] public Transform firePoint;

    private float fireTimer = 0f;

    void Update()
    {
        fireTimer += Time.deltaTime;

        if (fireTimer < 1f / fireRate) return;

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

        if (nearest != null)
        {
            GameObject p = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
            Projectile proj = p.GetComponent<Projectile>();
            proj.target = nearest;
            fireTimer = 0f;
        }
    }
}

