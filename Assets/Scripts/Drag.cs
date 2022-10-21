using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drag : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public delegate void Moved();
    public Moved movedEvent;
    private CanvasGroup canvasGroup;
    private Canvas canvas;
    private RectTransform rectTransform;
    private Transform parent;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        parent = transform.parent;
        if (transform.parent.TryGetComponent(out CardsConteiner cardsConteiner))
        {
            transform.parent = canvas.transform;
            cardsConteiner.RemoveLastCard();
        }
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.localPosition = Vector3.zero;
        canvasGroup.blocksRaycasts = true;
    }
    private void OnEnable()
    {
        canvasGroup.blocksRaycasts = true;
    }
    private void OnDisable()
    {
        canvasGroup.blocksRaycasts = false;
    }
}
