using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{
    [Header("Stats")]
    public float speed = 2f;
    public int maxHealth = 10;
    public int reward = 5;

    private int currentHealth;
    private int currentWaypoint = 0;

    private Transform[] eventosclave;

    public event Action<Enemy> Muerto;
    public event Action<Enemy> LlegarFinal;

    public void Initialize(Transform[] path)
    {
        eventosclave = path;
        currentHealth = maxHealth;
    }

    void Update()
    {
        Move();
    }

    void Move()  //Movimiento del enemigo
    {
        if (eventosclave == null || eventosclave.Length == 0) return;

        Transform target = eventosclave[currentWaypoint];

        transform.position = Vector3.MoveTowards(transform.position,target.position,speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            currentWaypoint++;

            if (currentWaypoint >= eventosclave.Length)
            {
                ReachEnd();
            }
        }
    }

    public void TakeDamage(int damage) //Esta es la parte de recibir daño
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die() //Esta es la parte de cuando t mueres
    {
        Muerto?.Invoke(this);
        Destroy(gameObject);
    }

    void ReachEnd() 
    {
        LlegarFinal?.Invoke(this);
        Destroy(gameObject);
    }
}