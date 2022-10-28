using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClearLineBonus : Bonus
{
    [SerializeField] private CardsConteiner[] cardsConteiners;
    [SerializeField] private List<CardsConteiner> cardsConteinersNotEmpty = new List<CardsConteiner>();
    [SerializeField] private Deck deck;
    [SerializeField] private Health health;
    [SerializeField] private UIBonus UIBonusRender;
    [SerializeField] private BonusShop bonusShop;
    private bool mayUse = false;

    private void Start()
    {
        UIBonusRender = GetComponent<UIBonus>();
        UIBonusRender.ChangeColorOnNotActive();
        UIBonusRender.RenderImageBonus(spriteBonus);
        health.loseGameEvent += ResetBonus;
        ResetBonus();
    }
    public override bool Entry()
    {
        if (countBonus > 0)
        {
            for (int i = 0; i < cardsConteiners.Length; i++)
            {
                if (cardsConteiners[i].CardsInConteiner.Count > 0)
                {
                    cardsConteinersNotEmpty.Add(cardsConteiners[i]);
                    mayUse = true;
                }
            }
        }
        else
        {
            bonusShop.ActivatePanelShop(this);
        }
        if (mayUse)
        {
            for (int i = 0; i < cardsConteinersNotEmpty.Count; i++)
            {
                cardsConteinersNotEmpty[i].GetComponent<Button>().enabled = true;
                cardsConteinersNotEmpty[i].clearedLineEvent += Use;
            }
            deck.CurrentActiveCard.GetComponent<Drag>().enabled = false;
            UIBonusRender.ChangeColorOnActive();
        }
        return mayUse;
    }
    public override void Use()
    {
        countBonus--;
        countUseInGame++;
        UIBonusRender.RenderCountBonus(countBonus);
        Exit();
    }

    public override void Exit()
    {
        UIBonusRender.ChangeColorOnNotActive();
        int countConteiners = cardsConteinersNotEmpty.Count - 1;
        for (int i = countConteiners; i >= 0; i--)
        {
            cardsConteinersNotEmpty[i].GetComponent<Button>().enabled = false;
            cardsConteinersNotEmpty[i].clearedLineEvent -= Use;
            cardsConteinersNotEmpty.RemoveAt(i);
        }
        deck.CurrentActiveCard.GetComponent<Drag>().enabled = true;
        mayUse = false;
        exitBonusEvent?.Invoke();
    }
    public void ResetBonus()
    {
        countBonus = 2;
        UIBonusRender.RenderCountBonus(countBonus);
    }
}
