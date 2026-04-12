using UnityEngine;

public class FastEnemy : Enemy
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _speed = 3.5f;
        _health = 4;
        _reward = 10;
        
    }
   
}
