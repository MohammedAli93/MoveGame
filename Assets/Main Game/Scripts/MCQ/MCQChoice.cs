using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class MCQChoice : MonoBehaviour, IPointerClickHandler
{
    public bool isCorrect;

    [SerializeField] UnityEvent OnSelected;
    [SerializeField] UnityEvent OnDeselected;

    [HideInInspector] public UnityAction<MCQChoice> OnChoiceClicked;
    [HideInInspector] public bool isSelected
    {
        get
        {
            return _isSelected;
        }
        set
        {
            _isSelected = value;
            if (value)
                OnSelected?.Invoke();
            else
                OnDeselected?.Invoke();
        }
    }

    private bool _isSelected = false;

    public void OnPointerClick(PointerEventData eventData)
    {
        isSelected = !isSelected;
        OnChoiceClicked?.Invoke(this);
    }
}
