using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIBonus : MonoBehaviour
{
    [SerializeField] private Image imageConteiner;
    [SerializeField] private Image colorImage;
    [SerializeField] private TMP_Text countText;
    [SerializeField] private Color commonColor;
    [SerializeField] private Color useColor;

    private void Start()
    {
        colorImage = GetComponent<Image>();
        colorImage.color = commonColor;
    }
    public void RenderImageBonus(Sprite sprite)
    {
        imageConteiner.sprite = sprite;
    }
    public void RenderCountBonus(int count)
    {
        countText.text = count.ToString();
    }
    public void ChangeColorOnActive()
    {
        colorImage.color = useColor;
    }
    public void ChangeColorOnNotActive()
    {
        colorImage.color = commonColor;
    }
}
