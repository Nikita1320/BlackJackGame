using UnityEngine;
using UnityEngine.EventSystems;

public class Drop : MonoBehaviour, IDropHandler
{
    private CardsConteiner cardsConteiner;
    private void Start()
    {
        cardsConteiner = GetComponent<CardsConteiner>();
    }
    public void OnDrop(PointerEventData eventData)
    {
        Drag dragCard = eventData.pointerDrag.GetComponent<Drag>();
        cardsConteiner.AddCard(dragCard.GetComponent<Card>());
        if (dragCard.transform.parent.TryGetComponent(out CardsConteiner oldCardsConteiner))
        {
            oldCardsConteiner.RemoveLastCard();
        }
        dragCard.transform.parent = transform;
        CanvasGroup dragCanvasGroup = GetComponent<CanvasGroup>();
        dragCard.movedEvent?.Invoke();
        dragCard.enabled = false;
    }
}
