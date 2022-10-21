using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardsConteiner : MonoBehaviour
{
    public delegate void AddedCard();
    public AddedCard addedCardEvent;
    public delegate void LoseLine();
    public LoseLine loseLineEvent;
    public delegate void WinLine(List<Card> cards);
    public WinLine winLineEvent;
    public delegate void AddedCardNotAffect();
    public AddedCardNotAffect addedCardNotAffectEvent;

    [SerializeField] private List<Card> cards;
    [SerializeField] private int winScore;
    [SerializeField] private TMP_Text currentScoreText;
    [SerializeField] private int[] currentScoreLine = new int[2];
    [SerializeField] private int[,] pointLine = new int[2, 5];

    public void AddCard(Card card)
    {
        cards.Add(card);
        addedCardEvent?.Invoke();
        if (currentScoreLine[0] + card.Point[0] <= 21 && currentScoreLine[1] + card.Point[1] <= 21)
        {
            pointLine[0,cards.Count - 1] = card.Point[0];
            currentScoreLine[0] += card.Point[0];

            pointLine[1, cards.Count - 1] = card.Point[1];
            currentScoreLine[1] += card.Point[1];
        }
        else
        {
            pointLine[0, cards.Count - 1] = card.Point[0];
            currentScoreLine[0] += card.Point[0];

            pointLine[1, cards.Count - 1] = card.Point[0];
            currentScoreLine[1] += card.Point[0];
        }
        RenderScoreConteiner();
        if ((cards.Count == 5 && (currentScoreLine[0] <= 21 || currentScoreLine[1] <= 21)) || (currentScoreLine[0] == 21 || currentScoreLine[1] == 21))
        {
            CollectWinningValue();
        }
        else if (currentScoreLine[0] > 21)
        {
            OverflowScoreLine();
        }
        else
        {
            addedCardNotAffectEvent?.Invoke();
        }
    }
    private void RenderScoreConteiner()
    {
        currentScoreText.text = "";
        if (currentScoreLine[0] == currentScoreLine[1])
        {
            currentScoreText.text = currentScoreLine[0].ToString();
        }
        else
        {
            currentScoreText.text = currentScoreLine[0].ToString();
            if (currentScoreLine[1] <= 21)
            { 
                currentScoreText.text += "/" + currentScoreLine[1].ToString();
            }
        }
    }
    public void RemoveLastCard()
    {
        currentScoreLine[0] -= pointLine[0,cards.Count - 1];
        currentScoreLine[1] -= pointLine[1, cards.Count - 1];
        pointLine[1, cards.Count - 1] = 0;
        pointLine[1, cards.Count - 1] = 0;
        cards.RemoveAt(cards.Count - 1);
        RenderScoreConteiner();
    }
    public void OverflowScoreLine()
    {
        loseLineEvent?.Invoke();
        ClearLine();
    }
    public void CollectWinningValue()
    {
        winLineEvent?.Invoke(cards);
        ClearLine();
    }
    public void ClearLine()
    {
        currentScoreLine[0] = 0;
        currentScoreLine[1] = 0;
        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                pointLine[i, j] = 0;
            }
        }

        int t = cards.Count;
        for (int i = t - 1; i >= 0; i--)
        {
            Destroy(cards[i].gameObject);
            cards.RemoveAt(i);
        }
        RenderScoreConteiner();
    }
}
