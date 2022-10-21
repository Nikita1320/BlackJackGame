using UnityEngine;
public enum TypeCard
{
    Clubs,
    Diamonds,
    Hearts,
    Spades
}
public enum ColorCard
{
    Red,
    Black
}
public class Card : MonoBehaviour
{
    private TypeCard typeCard;
    private CardData cardData;
    public TypeCard TypeCard => typeCard;
    public Sprite SpriteCard => cardData.SpriteCard[(int)typeCard];
    public int[] Point
    {
        get
        {
            int[] temp =  new int[2];
            if (cardData.Point.Length <= 1)
            {
                temp[0] = cardData.Point[0];
                temp[1] = cardData.Point[0];
            }
            else
            {
                temp[0] = cardData.Point[0];
                temp[1] = cardData.Point[1];
            }
            return temp;
        }
    }
    public ColorCard ColorCard
    {
        get
        {
            if (typeCard == TypeCard.Diamonds || typeCard == TypeCard.Hearts)
            {
                return ColorCard.Red;
            }
            else
            {
                return ColorCard.Black;
            }
        }
    }
    public void Init(CardData _cardData)
    {
        if (cardData == null)
        {
            cardData = _cardData;
            typeCard = (TypeCard)Random.Range(0, 4);
        }
    }
}
