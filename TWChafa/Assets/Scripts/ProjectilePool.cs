using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class ProjectilePool : MonoBehaviour
{
    public Projectile projectilePrefab;
    public int initialSize = 10;

    private readonly Queue<Projectile> _availableObjects = new Queue<Projectile>();

    void Start()
    {
        for (int i = 0; i < initialSize; i++)
        {
            Projectile p = Instantiate(projectilePrefab);
            p.transform.SetParent(transform);
            p.gameObject.SetActive(false);
            p.SetPool(this);
            _availableObjects.Enqueue(p);
        }
    }

    public Projectile GetObject()
    {
        Projectile objectToReturn;

        if (_availableObjects.Count > 0)
        {
            objectToReturn = _availableObjects.Dequeue();
        }
        else
        {
            objectToReturn = Instantiate(projectilePrefab);
            objectToReturn.SetPool(this);
        }

        objectToReturn.gameObject.SetActive(true);
        return objectToReturn;
    }

    public void ReturnObject(Projectile projectile)
    {
        projectile.gameObject.SetActive(false);
        _availableObjects.Enqueue(projectile);
    }
}
