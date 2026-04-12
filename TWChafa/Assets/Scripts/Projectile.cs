using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class Projectile : MonoBehaviour
{
    private ProjectilePool _pool;
    public GameObject _target;
    public float _speed = 10f;
    public int _damage = 1;

    public void SetPool(ProjectilePool pool)
    {
        _pool = pool;
    }

    public void Launch(GameObject newTarget)
    {
        _target = newTarget;
    }

    void Update()
    {
        if (_target == null)
        {
            ReturnToPool();
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, _target.transform.position, _speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, _target.transform.position) < 0.2f)
        {
            Enemy enemy = _target.GetComponent<Enemy>();

            if (enemy != null)
            {
                enemy.TakeDamage(_damage);
            }

            ReturnToPool();
        }
    }

    private void ReturnToPool()
    {
        _target = null;

        if (_pool != null)
        {
            _pool.ReturnObject(this);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}