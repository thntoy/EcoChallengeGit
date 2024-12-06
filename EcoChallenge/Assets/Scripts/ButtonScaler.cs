using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonScaler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
{
    private Vector3 _originalScale;
    private bool _isSelected;

    void Start()
    {
        _originalScale = transform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!_isSelected)
        {
            transform.localScale = _originalScale * 1.1f;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!_isSelected)
        {
            transform.localScale = _originalScale;
        }
    }

    public void OnSelect(BaseEventData eventData)
    {
        _isSelected = true;
        transform.localScale = _originalScale * 1.1f;
    }

    public void OnDeselect(BaseEventData eventData)
    {
        _isSelected = false;
        transform.localScale = _originalScale;
    }
}