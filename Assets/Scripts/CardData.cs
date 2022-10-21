using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCard", menuName = "Cards")]
public class CardData : ScriptableObject
{
    [SerializeField] private Sprite[] spriteCard;
    [SerializeField] private int[] point;
    public Sprite[] SpriteCard => spriteCard;
    public int[] Point => point;
}
