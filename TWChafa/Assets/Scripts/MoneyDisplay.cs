using TMPro;
using UnityEngine;

public class MoneyDisplay : MonoBehaviour
{
    public TextMeshProUGUI moneyText;

    void OnEnable()
    {
       
        Invoke("StartListening", 0.1f);
    }

    void StartListening()
    {
        if (EconomyManager.Instance != null)
        {
         
            EconomyManager.Instance.OnMoneyChanged += UpdateMoneyText;
            UpdateMoneyText(EconomyManager.Instance.money);
        }
    }

   
    void UpdateMoneyText(int currentMoney)
    {
        if (moneyText != null)
        {
            moneyText.text = "$ " + currentMoney;
        }
    }

    void OnDisable()
    {
        if (EconomyManager.Instance != null)
        {
            EconomyManager.Instance.OnMoneyChanged -= UpdateMoneyText;
        }
    }
}