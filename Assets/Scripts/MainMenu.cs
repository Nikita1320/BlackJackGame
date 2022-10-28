using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private List<Button> menuItems = new();
    private Button menuButton;
    private bool animationComplete;
    [SerializeField] private float offsetButton;
    [SerializeField] private float timePlayAnimation;
    private bool isActivMenu = false;
    public void Start()
    {
        menuButton = GetComponent<Button>();
    }
    public void ChangeActivityState()
    {
        if (isActivMenu == false)
        {
            isActivMenu = true;
            for (int i = 0; i < menuItems.Count; i++)
            {
                menuItems[i].transform.DOLocalMoveY(offsetButton * (i + 1), timePlayAnimation);
            }
        }
        else
        {
            isActivMenu = false;
            for (int i = 0; i < menuItems.Count; i++)
            {
                menuItems[i].transform.DOMoveY(transform.position.y, timePlayAnimation);
            }
        }
    }
}
