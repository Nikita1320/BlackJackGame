using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Wallet: MonoBehaviour
{
    [SerializeField] private TMP_Text moneyText;
    [SerializeField] private int countMoneyInBank;
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private int dividerValueForTransitScoreToMoney;
    private SaveAndLoad saveAndLoad = new SaveAndLoad();
    private void Start()
    {
        scoreManager.gameLosedWithScoreEvent += AddCoinWithScoreValue;
        countMoneyInBank = saveAndLoad.LoadMoney();
        RenderCountMoney();
    }
    public void AddCoin(int countMoney)
    {
        countMoneyInBank += countMoney;
        saveAndLoad.SaveMoney(countMoneyInBank);
        RenderCountMoney();
    }
    private void AddCoinWithScoreValue(int score)
    {
        countMoneyInBank += (score / dividerValueForTransitScoreToMoney);
        saveAndLoad.SaveMoney(countMoneyInBank);
        RenderCountMoney();
    }
    private void RenderCountMoney()
    {
        moneyText.text = countMoneyInBank.ToString();
    }
    public bool Buy(int cost)
    {
        if (cost < countMoneyInBank)
        {
            countMoneyInBank -= cost;
            return true;
        }
        return false;
    }
}
