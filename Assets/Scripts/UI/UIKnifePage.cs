using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIKnifePage : UIPage
{

    [SerializeField]
    protected UIButton back;
    
    [SerializeField]
    protected Image skin;
    [SerializeField]
    protected UIShopGroupElement[] groups;

    private void Awake()
    {
        back.BindOnClick(Back);
        for (int i = 0; i < groups.Length; i++)
        {
            groups[i].BindOnSelect(OnSelectItem);
        }
    }
    protected void Back(PointerEventData eventData)
    {
        system.Back();
    }
    protected void OnSelectItem(StuffSkinScriptableObject skin)
    {
        this.skin.sprite = skin.Sprites[0];
        GameInstance.Instance.SetCurrentSkinName(skin.name);
        GameInstance.Instance.GameState.SetKnifeSkin(skin);
    }
    protected override void OnOpen()
    {
        base.OnOpen();
        skin.sprite = GameInstance.Instance.GameState.GetKnifeSkin().Sprites[0];
        if (!groups[0].HasSkins)
        {
            List<StuffSkinInfo> skins = new List<StuffSkinInfo>();
            StuffSkinScriptableObject[] r = GameInstance.Instance.GameState.LevelLoader.LoadAllKnifeSkins();
            for (int i = 0; i < r.Length; i++)
            {
                StuffSkinScriptableObject skin = r[i];
                skins.Add(new StuffSkinInfo(skin, GameInstance.Instance.HasSkin(skin)));
            }
            groups[0].SetSkins(skins);
        }


    }

}
