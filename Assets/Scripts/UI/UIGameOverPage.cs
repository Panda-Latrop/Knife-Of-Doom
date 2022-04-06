using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class UIGameOverPage : UIPage
{
    [SerializeField]
    protected UIButton toMainMenu,restart;

    private void Awake()
    {
        toMainMenu.BindOnClick(ToMainMenu);
        restart.BindOnClick(Restart);
    }
    
    protected void Restart(PointerEventData eventData)
    {
        GameInstance.Instance.GameState.Restart();
        system.Back();
    }
    protected void ToMainMenu(PointerEventData eventData)
    {
        SceneManager.LoadScene(0);
    }
}
