using UnityEngine;

public class Tower : MonoBehaviour
{
    [Header("Ataque")]
    [SerializeField] private float range = 5f;
    [SerializeField] private float fireRate = 1f;

    [Header("Referencias")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;

    private float fireTimer = 0f;

    private void Update()
    {
        fireTimer += Time.deltaTime;

        if (fireTimer >= 1f / fireRate)
        {
            ShootNearestEnemy();
        }
    }

    private void ShootNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        GameObject nearestEnemy = null;
        float nearestDistance = float.MaxValue;

        for (int i = 0; i < enemies.Length; i++)
        {
            float distance = Vector3.Distance(transform.position, enemies[i].transform.position);

            if (distance <= range && distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestEnemy = enemies[i];
            }
        }

        if (nearestEnemy == null)
        {
            return;
        }

        GameObject newProjectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);

        Projectile projectileScript = newProjectile.GetComponent<Projectile>();

        if (projectileScript != null)
        {
            projectileScript.target = nearestEnemy;
        }

        fireTimer = 0f;
    }
}