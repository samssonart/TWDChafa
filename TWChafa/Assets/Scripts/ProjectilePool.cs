using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class ProjectilePool : MonoBehaviour
{
    public static ProjectilePool Instance;

    public GameObject _projectilePrefab;
    public int _initialSize = 10;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Queue<GameObject> pool = new Queue<GameObject>();

    void Awake()
    {
      if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
      Instance = this;
    }
    void Start()
    {
        if (_projectilePrefab == null)
        {
            Debug.LogError("ProjectilePool no esta asignado");
            return;
        }

        for (int i = 0; i < _initialSize; i++)
        {
            GameObject projectile = Instantiate(_projectilePrefab);
            projectile.SetActive(false);
            pool.Enqueue(projectile);
        }

    }

    public GameObject GetProjectile()
    {
        if (pool.Count > 0)
        {
            GameObject projectile = pool.Dequeue();
            projectile.SetActive(true);
            return projectile;
        }

        GameObject newProjectile = Instantiate(_projectilePrefab, transform); 
        return newProjectile;

    }

    // Update is called once per frame
    public void ReturnProjectile(GameObject projectile)
    {
        if (projectile == null)
        {  
            return; 
        }  

        projectile.SetActive(false);
        pool.Enqueue(projectile);
        
    }
}
