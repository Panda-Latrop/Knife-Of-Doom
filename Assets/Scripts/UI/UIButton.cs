using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIButton : MonoBehaviour, IPointerClickHandler
{
    public delegate void ButtonClickDelegate(PointerEventData eventData);
    protected ButtonClickDelegate OnClick;
    public void OnPointerClick(PointerEventData eventData)
    {
        CallOnClick(eventData);
    }
    public void CallOnClick(PointerEventData eventData)
    {
        OnClick?.Invoke(eventData);
    }
    public void BindOnClick(ButtonClickDelegate action)
    {
        OnClick += action;
    }
    public void UnbindOnClick(ButtonClickDelegate action)
    {
        OnClick -= action;
    }
    public void ClearOnClick()
    {
        OnClick = null;
    }
    private void OnDestroy()
    {
        OnClick = null;
    }
}
