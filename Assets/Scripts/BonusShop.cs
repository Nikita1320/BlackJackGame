using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class BonusShop : MonoBehaviour
{
    [SerializeField] private Wallet wallet;
    [SerializeField] private Image imageBonus;
    [SerializeField] private TMP_Text priceForBuyBonusText;
    private Bonus currentBonus;
    [SerializeField] private TMP_Text reportText;
    [SerializeField] private string notEnoughMoneyText;
    [SerializeField] private string successfulBuyText;
    [SerializeField] private float timeAnimationReportText;
    private Color colorReportText;
    private void Start()
    {
        colorReportText = reportText.color;
    }
    public void ActivatePanelShop(Bonus bonus)
    {
        currentBonus = bonus;
        this.gameObject.SetActive(true);
        imageBonus.sprite = bonus.SpriteBonus;
        priceForBuyBonusText.text = bonus.Price.ToString();
    }
    public void BuyBonus()
    {
        if (wallet.Buy(currentBonus.Price))
        {
            currentBonus.AddBonusCount(1);
            TweenReportText(successfulBuyText);
        }
        else
        {
            TweenReportText(notEnoughMoneyText);
        }
    }
    private void TweenReportText(string message)
    {
        reportText.text = message;
        reportText.color = colorReportText;
        reportText.DOFade(0, timeAnimationReportText);
    }
}
