using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIWallet : MonoBehaviour
{
    [SerializeField] private TMP_Text moneyText;
    public void RenderCountMoney(int countMoneyInBank)
    {
        moneyText.text = countMoneyInBank.ToString();
    }
}
