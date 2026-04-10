using UnityEngine;

public class FastEnemy : EnemyBase
{
    public float speedMultiplierEnEnojo = 1.5f;

    public override void TakeDamage(int damage)
    {
      
        speed *= speedMultiplierEnEnojo;

     
        base.TakeDamage(damage);
    }
}
