using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILine : MonoBehaviour
{
    public Vector2 startPoint;
    public Vector2 endPoint;
    public float width = 0.2f;
    public Color c = Color.white;
    Image line => GetComponent<Image>();

    void UpdateLine()
    {
        line.color = c;
        line.GetComponent<RectTransform>().localScale = new Vector3(width, Vector2.Distance(startPoint, endPoint), 1);
        line.transform.position = startPoint + (Vector2.Distance(startPoint,endPoint)/2) * (endPoint - startPoint);
        float zRotation = Vector2.SignedAngle(endPoint - startPoint, Vector2.up);
        line.transform.localEulerAngles = new Vector3(0, 0, zRotation);
    }

    public void SetStartPoint(Vector2 point)
    {
        startPoint = point;
        UpdateLine();
    }

    public void SetEndPoint(Vector2 point)
    {
        endPoint = point;
        UpdateLine();
    }

    public void SetWidth(float width)
    {
        this.width = width;
        UpdateLine();
    }

    public void SetColor(Color c)
    {
        this.c = c;
        UpdateLine();
    }

}
