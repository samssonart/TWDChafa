using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Tower : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private float fireRate = 1f;
    [SerializeField] private int damage = 1;
    public GameObject projectilePrefab;
    public Transform firePoint;

    [Header("Targeting")]
    [SerializeField] private TargetPriority targetPriority = TargetPriority.Closest;
    [SerializeField] private float range = 5f;

    private List<Enemy> enemiesInRange = new();
    private float fireTimer = 0f;
    private SphereCollider rangeCollider;

    public enum TargetPriority { First, Closest, Strongest }

    public float Range => range; // Para acceso externo si necesitas

    void Start()
    {
        rangeCollider = gameObject.AddComponent<SphereCollider>();
        rangeCollider.isTrigger = true;
        rangeCollider.radius = range;
    }

    void Update()
    {
        fireTimer += Time.deltaTime;
        if (fireTimer < 1f / fireRate || enemiesInRange.Count == 0) return;

        Enemy target = GetTarget();
        if (target != null)
        {
            Fire(target);
            fireTimer = 0f;
        }
    }

    Enemy GetTarget()
    {
        enemiesInRange.RemoveAll(e => e == null);
        return targetPriority switch
        {
            TargetPriority.First => enemiesInRange.FirstOrDefault(),
            TargetPriority.Closest => enemiesInRange.OrderBy(e => Vector3.Distance(transform.position, e.transform.position)).FirstOrDefault(),
            TargetPriority.Strongest => enemiesInRange.OrderByDescending(e => e.data.health).FirstOrDefault(),
            _ => null
        };
    }

    void Fire(Enemy target)
    {
        GameObject proj = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        Projectile projectile = proj.GetComponent<Projectile>();
        projectile.Initialize(target.gameObject, damage);
        fireTimer = 0f;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
            enemiesInRange.Add(other.GetComponent<Enemy>());
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
            enemiesInRange.Remove(other.GetComponent<Enemy>());
    }
}
