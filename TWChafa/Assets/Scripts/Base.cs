using UnityEngine;

public class Base : MonoBehaviour
{
    public float healt = 10f;

    public void RecibirDaño(int damage)
    {
        healt -= damage;
        GameManager.Instance.LoseLife(damage);
        if (healt <= 0)
        { 
            Debug.Log("Ya valiste.");
            Time.timeScale = 0f;
        }
    }
}
