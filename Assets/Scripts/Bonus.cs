using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public abstract class Bonus : MonoBehaviour
{
    public delegate void ExitBonus();
    public ExitBonus exitBonusEvent;
    [SerializeField] private int price;
    [SerializeField] protected Sprite spriteBonus;
    [SerializeField] protected int countBonus;
    [SerializeField] protected int countUseInGame;
    public int CountUseInGame => countUseInGame;
    public int Price => price * countUseInGame;
    public Sprite SpriteBonus => spriteBonus;
    public void AddBonusCount(int count)
    {
        countBonus += count;
    }
    public abstract bool Entry();
    public abstract void Use();
    public abstract void Exit();
}
