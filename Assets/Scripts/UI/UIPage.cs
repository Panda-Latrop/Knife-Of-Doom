using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPage : MonoBehaviour
{
    [SerializeField]
    protected Canvas canvas;
    protected UISystem system;
    public void SetSystem(UISystem system)
    {
        this.system = system;
    }
    public void SetVisability(bool visable)
    {
        if (visable) OnOpen(); else OnClose();
    }
    protected virtual void OnOpen()
    {
        canvas.enabled = true;
    }
    protected virtual void OnClose()
    {
        canvas.enabled = false;
    }
}
