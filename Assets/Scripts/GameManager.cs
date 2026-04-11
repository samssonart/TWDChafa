using TMPro;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int dinero {get; private set;} = 100;
    public int Vidas {get; private set;} = 10;


    public event Action OnStatsChanged;
    public event Action OnGameOver;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void AddMoney(int amount)
    {
        dinero += amount;
        NotifyChanges();
    }

    public bool SpendMoney(int amount)
    {
        if (dinero < amount)
            return false;

        dinero -= amount;
        NotifyChanges();
        return true;
    }

    public void LoseLife(int amount)
    {
        Vidas -= amount;
        NotifyChanges();

        if (Vidas <= 0)
        {
            Perdiste();
        }
    }

    void NotifyChanges()
    {
        OnStatsChanged?.Invoke();
    }

    void Perdiste()
    {
        Debug.Log("Perdiste ...");
        Time.timeScale = 0f;
        OnGameOver?.Invoke();
    }
}