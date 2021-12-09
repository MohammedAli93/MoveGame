using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

[RequireComponent(typeof(CanvasGroup))]
public class ColorPicker : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private GameObject currentDraggedGameobject;
    private CanvasGroup currentDraggedCanvasGroup;

    [HideInInspector] public UnityAction OnPiecePut;
    [HideInInspector] public UnityAction OnPieceThrown;

    [ContextMenu("Spawn Color")]
    GameObject SpawnColor()
    {
        GameObject go = Instantiate(gameObject, transform);
        DestroyImmediate(go.GetComponent<ColorPicker>());
        return go;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        currentDraggedGameobject = SpawnColor();
        currentDraggedCanvasGroup = currentDraggedGameobject.GetComponent<CanvasGroup>();
        currentDraggedCanvasGroup.alpha = 0.6f;
        currentDraggedCanvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        currentDraggedGameobject.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        EmptySlot emptySlot = eventData.pointerCurrentRaycast.gameObject?.GetComponent<EmptySlot>();

        if (emptySlot == null || emptySlot.isOcuppied)
        {
            OnPieceThrown?.Invoke();

            Destroy(currentDraggedGameobject);
        }
        else
        {
            currentDraggedGameobject.transform.position = emptySlot.transform.position;
            currentDraggedGameobject.transform.parent = emptySlot.transform;
            emptySlot.isOcuppied = true;

            currentDraggedCanvasGroup.alpha = 1f;
            currentDraggedCanvasGroup.blocksRaycasts = true;

            currentDraggedGameobject = null;
            currentDraggedCanvasGroup = null;
            
            OnPiecePut?.Invoke();
        }
    }
}
