using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider2D))]
public class PieceBehaviour : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public bool shouldOverrideHalfAndHalfBehaviour = false;
    public int index = 0;
    public bool isLeft;
    public LayerMask piecesLayer;
    private RectTransform rectTransform => GetComponent<RectTransform>();
    private BoxCollider2D col => GetComponent<BoxCollider2D>();

    public bool isMovementLocked = false;
    public static UnityAction OnAttachingAPiece;

    private void Start()
    {
        col.size = new Vector2(rectTransform.rect.width, rectTransform.rect.height);
        col.isTrigger = true;

        if (shouldOverrideHalfAndHalfBehaviour) return;

        if (isLeft)
        {
            col.offset = new Vector2(rectTransform.sizeDelta.x, 0);
        }
        else
        {
            col.offset = new Vector2(-rectTransform.sizeDelta.x, 0);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {

    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isMovementLocked) return;
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (isMovementLocked) return;

        Collider2D[] contactColliders = Physics2D.OverlapBoxAll(transform.position, col.bounds.size, 0, piecesLayer);

        foreach (var c in contactColliders)
        {
            if (c && c != col)
            {
                PieceBehaviour piece = c.GetComponent<PieceBehaviour>();

                if (piece && piece.index == index)
                {
                    transform.position = c.bounds.center;
                    transform.SetParent(c.transform, true);
                    isMovementLocked = true;
                    piece.isMovementLocked = true;
                    OnAttachingAPiece?.Invoke();
                }
            }
        }
    }
}
