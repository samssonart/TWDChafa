using UnityEngine;

public class Health : MonoBehaviour
{
    public int health = 10;
    public int reward = 5;

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
