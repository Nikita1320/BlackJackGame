using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MoveLastCardsBonus : Bonus
{
    [SerializeField] Health health;
    [SerializeField] Deck deck;
    [SerializeField] private CardsConteiner[] cardsConteiners;
    [SerializeField] private List<Card> lastCardEveryConteiners;
    [SerializeField] private UIBonus UIBonusRender;
    [SerializeField] private BonusShop bonusShop;
    private bool mayUse = false;

    private void Start()
    {
        UIBonusRender = GetComponent<UIBonus>();
        UIBonusRender.RenderImageBonus(spriteBonus);
        health.loseGameEvent += ResetBonus;
        UIBonusRender.ChangeColorOnNotActive();
        ResetBonus();
    }
    public override bool Entry()
    {
        if (countBonus > 0)
        {
            for (int i = 0; i < cardsConteiners.Length; i++)
            {
                if (cardsConteiners[i].CardsInConteiner.Count != 0)
                {
                    lastCardEveryConteiners.Add(cardsConteiners[i].LastCard);
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
            for (int i = 0; i < lastCardEveryConteiners.Count; i++)
            {
                lastCardEveryConteiners[i].GetComponent<Drag>().enabled = true;
                lastCardEveryConteiners[i].GetComponent<Drag>().movedEvent += Use;
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
        int countConteiners = lastCardEveryConteiners.Count - 1;
        for (int i = countConteiners; i >= 0; i--)
        {
            if (lastCardEveryConteiners[i] != null)
            {
                lastCardEveryConteiners[i].GetComponent<Drag>().enabled = false;
                lastCardEveryConteiners[i].GetComponent<Drag>().movedEvent -= Use;
                lastCardEveryConteiners.RemoveAt(i);
            }
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
