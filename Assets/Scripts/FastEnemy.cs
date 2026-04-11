using UnityEngine;

public class FastEnemy : Enemy
{
    void Start()
    {
        speed = 4f;
        maxHealth = 5;
        reward = 8;
    }
}