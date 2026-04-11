using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Enemy : MonoBehaviour, IDamageable
{
    public float _speed = 2f;
    public int _health = 10;
    public int _reward = 5;

    public GameObject[] _waypoints;
    public GameObject _goal;

    private int _currentWaypoint = 0;
    private bool _waygoal = false;

    protected virtual void Update()
    {
        if (!_waygoal)
        {
            if (_waypoints == null || _waypoints.Length == 0)
            {
                return;
            }

            if (_currentWaypoint < _waypoints.Length)
            {
                if (_waypoints[_currentWaypoint] == null)
                {
                    return;
                }

                Vector3 target = _waypoints[_currentWaypoint].transform.position;
                transform.position = Vector3.MoveTowards(transform.position, target, _speed * Time.deltaTime);

                if (Vector3.Distance(transform.position, target) < 0.1f)
                {
                    _currentWaypoint++;

                    if (_currentWaypoint >= _waypoints.Length)
                    {
                        _waygoal = true;
                    }
                }
            
            }
        }
        else
        {
            if (_goal == null)
            {
                return;
            }

            Vector3 target = _goal.transform.position;
            transform.position = Vector3.MoveTowards(transform.position, target, _speed * Time.deltaTime);

            if(Vector3.Distance(transform.position, target) < 0.5f)
            {
                End();
            }
        }
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;
        if (_health <= 0)
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.AddMoney(_reward);
            }
            Destroy(gameObject);
        }
    }

    public void SetPath(GameObject[] waypoints, GameObject goal)
    {
        _waypoints = waypoints;
        _goal = goal;
        _currentWaypoint = 0;
        _waygoal = false;
    }

    protected virtual void End()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.LoseLife(1);
        }

        Destroy(gameObject);

    }
}
