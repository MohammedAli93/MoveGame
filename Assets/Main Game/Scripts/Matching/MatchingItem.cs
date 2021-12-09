using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class MatchingItem : MonoBehaviour
{
    public int index = 0;
    public bool isMatched = false;
    public Color lineColor = Color.white;
    public static UnityAction OnMatching2Pieces;
    //[SerializeField] GameObject UILinePrefab;
    //UILine currentLine;
    LineRenderer line;



    public void OnBeginDrag(PointerEventData eventData)
    {
        if (isMatched) return;

        //GameObject go = Instantiate(UILinePrefab, transform);
        //currentLine = go.GetComponent<UILine>();

        //currentLine.SetWidth(0.5f);
        //currentLine.SetStartPoint(transform.position);
        //currentLine.SetEndPoint(transform.position);
        //currentLine.SetColor(lineColor);
        line = gameObject.AddComponent<LineRenderer>();
        line.positionCount = 2;
        line.SetPosition(0, transform.position);
        line.SetPosition(1, transform.position);
        line.startWidth = 0.2f;
        line.endWidth = 0.2f;
        line.startColor = Color.white;
        line.endColor = Color.white;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isMatched) return;
        //if (currentLine)
        //    currentLine.SetEndPoint(eventData.position);
        if (line)
            line.SetPosition(1, eventData.position);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (isMatched) return;
        if (!line) return;

        MatchingItem item = eventData.pointerCurrentRaycast.gameObject?.GetComponent<MatchingItem>();

        if (item == null || item.isMatched || item == this || item.index != index)
        {
            //Destroy(currentLine.gameObject);
            Destroy(line);
        }
        else
        {
            line.SetPosition(1, item.transform.position);
            item.isMatched = true;
            isMatched = true;
            OnMatching2Pieces?.Invoke();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Hi");
    }
}
