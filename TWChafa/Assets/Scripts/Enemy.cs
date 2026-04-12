using UnityEngine;


public class Enemy : MonoBehaviour
{
    public float speed = 2f;
    public int health = 10;
    public int reward = 5;

    public EnemyPath path;
    public Transform targetbase;
    public int Damage = 1;

    private int currentWaypoint = 0;
    private GameObject[] waypoints;

    void Start()
    {
        path = FindObjectOfType<EnemyPath>();

        GameObject baseObjt = GameObject.FindGameObjectWithTag("Base");
        if(baseObjt != null )
        {
            targetbase = baseObjt.transform;
        }
    }

    void Update()
    {
        if (path == null || path.waypoints.Length == 0) return;

        if(currentWaypoint < path.waypoints.Length)
        {
            Vector3 target = path.waypoints[currentWaypoint].position;
            MoveTo(target);
            if(Vector3.Distance(transform.position, target) < 0.1f)
            {
                currentWaypoint++;
            }
        }
        else
        {
            if (targetbase == null) return;

            MoveTo(targetbase.position);
        }
    }

    void MoveTo(Vector3 _target)
    {
        transform.position = Vector3.MoveTowards(transform.position, _target, speed * Time.deltaTime);

        Vector3 dir = (_target - transform.position).normalized;

        if (dir != Vector3.zero) 
        {
            transform.rotation = Quaternion.LookRotation(dir);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Base"))
        {
            Base basescript = other.GetComponent<Base>();
            if (basescript != null)
            {
                GameManager.Instance.LoseLife(1);
            }
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            GameManager.Instance.AddMoney(reward);
            Destroy(gameObject);
        }
    }
}
