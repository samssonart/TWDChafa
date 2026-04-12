using UnityEngine;

public class FastEnemy : Enemy
{
    private void Start()
    {
        speed = 4f;
        health = 6;
        reward = 5;
        damageToBase = 2;
    }
}