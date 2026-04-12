using UnityEngine;

public class Tower : MonoBehaviour
{
    public float _range = 5f;
    public float _fireRate = 1f;
    public ProjectilePool _projectilePrefab;
    public Transform _firePoint;

    private float _fireTimer = 0f;

    void Update()
    {
        if (_firePoint == null || _projectilePrefab == null)
        {
            return;
        } 

        _fireTimer += Time.deltaTime;

        if (_fireTimer < 1f / _fireRate)
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

            if (d < nearestDist && d <= _range)
            {
                nearest = e;
                nearestDist = d;
            }
        }

        if (nearest != null)
        {
            Projectile proj = _projectilePrefab.GetObject();
            proj.transform.position = _firePoint.position;
            proj.transform.rotation = Quaternion.identity;
            proj.Launch(nearest);
            _fireTimer = 0f;



        }
    }
}

