using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISystem : MonoBehaviour
{
    [SerializeField]
    protected UIPage[] pages;
    protected UIPage currentPage, prevuesPage;
    private void Awake()
    {
        for (int i = 0; i < pages.Length; i++)
        {
            pages[i].SetSystem(this);
        }
    }
    protected void Start()
    {
        prevuesPage = currentPage = pages[0];
        currentPage.SetVisability(true);
    }

    public void OpenPage(UIPage page)
    {
        prevuesPage = currentPage;
        currentPage = page;
        prevuesPage.SetVisability(false);
        currentPage.SetVisability(true);
    }
    public void Back()
    {
        UIPage page = prevuesPage;
        prevuesPage = currentPage;
        currentPage = page;
        prevuesPage.SetVisability(false);
        currentPage.SetVisability(true);
    }
}
