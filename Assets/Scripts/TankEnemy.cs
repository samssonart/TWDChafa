using UnityEngine;

public class TankEnemy : Enemy
{
    void Start()
    {
        speed = 1f;
        maxHealth = 20;
        reward = 15;
    }
}