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
        if (firePoint == null || projectilePrefab == null)
        {
            return;
        } 

        fireTimer += Time.deltaTime;

        if (fireTimer < 1f / fireRate)
        {
            return;
        }


        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject nearest = null;
        float nearestDist = float.MaxValue;

        foreach (GameObject e in enemies)
        {
            if (e == null || !e.activeInHierarchy)
            {
                continue;
            }
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

            if (proj != null)
            {
                proj.target = nearest;
                fireTimer = 0f;
            }
            
        }
    }
}

