using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public float fireRate = 1f;
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float projectileSpeed = 10f;
    public int damage = 2;

    private float fireTimer = 0f;
    private List<Enemy> enemiesInRange = new List<Enemy>();

    void Update()
    {
        fireTimer += Time.deltaTime;

        enemiesInRange.RemoveAll(e => e == null);

        if (enemiesInRange.Count > 0 && fireTimer >= 1f / fireRate)
        {
            Shoot(enemiesInRange[0]);
            fireTimer = 0;
        }
    }

    void Shoot(Enemy targetEnemy)
    {
        GameObject p = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        Projectile proj = p.GetComponent<Projectile>();
        
        if (proj != null)
        {
            proj.Setup(targetEnemy.transform, projectileSpeed, damage);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent<Enemy>(out Enemy enemy))
        {
            enemiesInRange.Add(enemy);
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.TryGetComponent<Enemy>(out Enemy enemy))
        {
            enemiesInRange.Remove(enemy);
        }
    }
}

