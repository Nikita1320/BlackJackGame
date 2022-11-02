using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class BackGroundShop : MonoBehaviour
{
    [SerializeField] private BackGround[] backGrounds;
    [SerializeField] private bool[] isUnlockedBackGrounds;
    [SerializeField] private Wallet wallet;
    [SerializeField] private Image mainBackGround;
    [SerializeField] private BackGround currentBackGround;
    [SerializeField] private TMP_Text reportText;
    [SerializeField] private string notEnoughMoneyText;
    [SerializeField] private string successfulBuyText;
    [SerializeField] private float timeAnimationReportText;
    [SerializeField] private Color activeColor;
    [SerializeField] private Color notActiveColor;
    private SaveAndLoad saveAndLoad;
    public bool[] loadBackgrounds;
    private void Awake()
    {
        saveAndLoad = new SaveAndLoad();
    }
    private void Start()
    {
        SelectBackGround(saveAndLoad.LoadCurrentBackgroundsBinary(0));
        Debug.Log("currentBack" + saveAndLoad.LoadCurrentBackgroundsBinary(0));
        isUnlockedBackGrounds = new bool[backGrounds.Length];
        isUnlockedBackGrounds[0] = true;

        loadBackgrounds = saveAndLoad.LoadBackgroundsBinary(isUnlockedBackGrounds);
        for (int i = 0; i < isUnlockedBackGrounds.Length && i < loadBackgrounds.Length; i++)
        {
            isUnlockedBackGrounds[i] = loadBackgrounds[i];
            if (isUnlockedBackGrounds[i])
            {
                backGrounds[i].OpenBackGround();
            }
        }
        transform.parent.gameObject.SetActive(false);
    }
    public void BuyBackGround(int index)
    {
        if (wallet.Buy(backGrounds[index].Price))
        {
            isUnlockedBackGrounds[index] = true;
            backGrounds[index].OpenBackGround();
            saveAndLoad.SaveBackgroundsBinary(isUnlockedBackGrounds);
            TweenReportText(successfulBuyText);
        }
        else
        {
            TweenReportText(notEnoughMoneyText);
        }
    }
    public void SelectBackGround(int index)
    {
        if (currentBackGround != null)
        {
            currentBackGround.GetComponent<Image>().color = notActiveColor;
        }
        currentBackGround = backGrounds[index];
        currentBackGround.GetComponent<Image>().color = activeColor;
        mainBackGround.sprite = currentBackGround.BackGroundSprite;
        saveAndLoad.SaveCurrentBackgroundsBinary(index);
        Debug.Log("currentBack" + saveAndLoad.LoadCurrentBackgroundsBinary(0));
    }
    private void TweenReportText(string message)
    {
        reportText.color = Color.red;
        reportText.text = message;
        reportText.DOFade(0, timeAnimationReportText);
    }
}
