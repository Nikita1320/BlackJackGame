using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BackGround : MonoBehaviour
{
    [SerializeField] private int price;
    [SerializeField] private Sprite backGroundSprite;
    [SerializeField] private Image slotShopImage;
    [SerializeField] private TMP_Text priceText;
    [SerializeField] private GameObject buyPanel;
    public int Price => price;
    public Sprite BackGroundSprite => backGroundSprite;
    private void Start()
    {
        slotShopImage.sprite = backGroundSprite;
        priceText.text = price.ToString();
    }
    public void OpenBackGround()
    {
        buyPanel.SetActive(false);
    }
}
