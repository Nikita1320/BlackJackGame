using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

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
    public delegate void ClearedLine();
    public ClearedLine clearedLineEvent;

    [SerializeField] private List<Card> cards;
    [SerializeField] private int winScore;
    [SerializeField] private TMP_Text currentScoreText;
    [SerializeField] private int[] currentScoreLine = new int[2];
    [SerializeField] private int[,] pointLine = new int[2, 5];
    [SerializeField] private Health health;
    [SerializeField] private float maxDelayToMoveCard;
    [SerializeField] private float minDelayToMoveCard;
    [SerializeField] private Transform moveTargetForClear;
    [SerializeField] private float maxJumpPowerClearAnimations;
    [SerializeField] private float minJumpPowerClearAnimations;
    [SerializeField] private float timePlayAnimationsClear;
    private Drop drop;
    private GridLayoutGroup gridLayoutGroup;
    private Coroutine clearCororutine;

    public Card LastCard => cards[cards.Count - 1];
    public List<Card> CardsInConteiner => cards;
    private void Start()
    {
        drop = GetComponent<Drop>();
        gridLayoutGroup = GetComponent<GridLayoutGroup>();
        health.loseGameEvent += ResetGame;
    }
    public void AddCard(Card card)
    {
        cards.Add(card);
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
        addedCardEvent?.Invoke();
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

        if (clearCororutine == null)
        {
            clearCororutine = StartCoroutine(ClearLineCoroutine());
        }

        RenderScoreConteiner();
        clearedLineEvent?.Invoke();
    }
    private IEnumerator ClearLineCoroutine()
    {
        if (cards.Count > 0)
        {
            drop.enabled = false;
            Card[] cardsToClear = new Card[cards.Count];
            for (int j = cards.Count - 1; j >= 0; j--)
            {
                cardsToClear[j] = cards[j];
                Debug.Log($"{cardsToClear[j]} / index = {j}");
                cards.RemoveAt(j);
            }
            int i = cardsToClear.Length - 1;
            while (true)
            {
                yield return new WaitForSeconds(Random.Range(minDelayToMoveCard, maxDelayToMoveCard));
                cardsToClear[i].transform.parent = moveTargetForClear;
                cardsToClear[i].transform.SetAsFirstSibling();
                TweenClearLine(cardsToClear[i].transform);
                if (i == 0)
                {
                    drop.enabled = true;
                    StopCoroutine(clearCororutine);
                    clearCororutine = null;
                }
                i--;
            }
        }
        else
        {
            clearCororutine = null;
        }
    }
    public void TweenClearLine(Transform cardTransform)
    {
        bool endAnimations = false;
        cardTransform.DOJump(moveTargetForClear.position, Random.Range(minJumpPowerClearAnimations, maxJumpPowerClearAnimations), 1, timePlayAnimationsClear);
        cardTransform.DORotate(new Vector3(0, 0, 360), timePlayAnimationsClear, RotateMode.FastBeyond360).SetRelative(true).SetEase(Ease.OutCirc)
            .OnComplete(() =>
        {
            if (endAnimations == false)
            {
                endAnimations = true;
                Destroy(cardTransform.gameObject);
            }
        });
    }
    public void ResetGame()
    {
        ClearLine();
    }
}
