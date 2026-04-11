using System.Collections.Generic;
using UnityEngine;
public class ProjectilePool : MonoBehaviour
{
    public static ProjectilePool Instance;

    public GameObject projectilePrefab;
    private Queue<GameObject> pool = new Queue<GameObject>();

    void Awake()
    {
        Instance = this;
    }

    public GameObject GetProjectile(Vector3 position, Quaternion rotation)
    {
        if (pool.Count > 0)
        {
            GameObject proj = pool.Dequeue();
            proj.transform.position = position;
            proj.transform.rotation = rotation;
            proj.SetActive(true);
            return proj;
        }
        else
        {
            return Instantiate(projectilePrefab, position, rotation);
        }
    }

    public void ReturnProjectile(GameObject proj)
    {
        proj.SetActive(false);
        pool.Enqueue(proj);
    }
}