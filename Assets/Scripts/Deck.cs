using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Deck : MonoBehaviour
{
    [SerializeField] private List<CardData> cardsData = new List<CardData>();
    [SerializeField] private List<CardsConteiner> cardsConteiner = new List<CardsConteiner>();
    [SerializeField] private GameObject prefabCard;
    [SerializeField] private GameObject deckPanel;
    [SerializeField] private Transform dragCardPosition;
    [SerializeField] private float timeTakeCard;
    [SerializeField] private float jumpPower;
    private Card currentCard;
    private bool spriteChanged = false;
    private void Start()
    {
        TakeCard();

    }
    public void TakeCard()
    {
        if (currentCard != null)
        {
            currentCard.GetComponent<Drag>().movedEvent -= TakeCard;
        }
        currentCard = Instantiate(prefabCard, deckPanel.transform).GetComponent<Card>();
        currentCard.transform.localPosition = Vector3.zero;
        currentCard.Init(cardsData[Random.Range(0, cardsData.Count)]);
        currentCard.transform.SetParent(dragCardPosition);
        currentCard.GetComponent<Drag>().movedEvent += TakeCard;

        spriteChanged = false;
        currentCard.transform.DOJump(dragCardPosition.position, jumpPower, 1, timeTakeCard).SetEase(Ease.Linear);
        currentCard.transform.DORotateQuaternion(Quaternion.Euler(0,90,0), timeTakeCard / 2)
              .OnComplete(() =>
              {
                  if (spriteChanged == false)
                  {
                      spriteChanged = true;
                      ChangeSprite(currentCard);
                      currentCard.transform.DORotateQuaternion(Quaternion.Euler(0, 0, 0), timeTakeCard / 2).OnComplete(() =>
                      {
                          currentCard.GetComponent<Drag>().enabled = true;
                      });
                  }
              });
    }
    public void ChangeSprite(Card card)
    {
        card.transform.rotation = Quaternion.Euler(0, -90, 0);
        card.GetComponent<Image>().sprite = card.SpriteCard;
    }
}
