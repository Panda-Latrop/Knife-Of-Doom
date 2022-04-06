using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class UIMainMenuPage : UIPage
{
    [SerializeField]
    protected UIButton startBattleLevel, toShop, setVibration;
    [SerializeField]
    protected UIPage shopPage;
    [SerializeField]
    protected Text score, maxStage;
    [SerializeField]
    protected SpriteRenderer knifeSkin;
    [SerializeField]
    protected Image vibrationButtomImage;
    [SerializeField]
    protected Sprite vibrationOn, vibrationOff;
    [SerializeField]
    protected int battleLevelID = 0;
    [SerializeField]
    protected string defaultMaxStageText = "KILL STREAK: ";
    public void Awake()
    {
        startBattleLevel.BindOnClick(StartBattleLevel);
        toShop.BindOnClick(ToShop);
        setVibration.BindOnClick(SetVibration);
    }
    protected override void OnOpen()
    {
        base.OnOpen();
        if (GameInstance.Instance.GetVibration())
            vibrationButtomImage.sprite = vibrationOn;
        else
            vibrationButtomImage.sprite = vibrationOff;
        int max = GameInstance.Instance.GetMaxStage();
        if (max > 1)
            maxStage.text = defaultMaxStageText + max;
        else
            maxStage.text = string.Empty;
        score.text = GameInstance.Instance.GetScore().ToString();
        knifeSkin.transform.gameObject.SetActive(true);
        knifeSkin.sprite = GameInstance.Instance.GameState.GetKnifeSkin().Sprites[0];
    }
    protected override void OnClose()
    {
        base.OnClose();
        knifeSkin.transform.gameObject.SetActive(false);
    }
    protected void StartBattleLevel(PointerEventData eventData)
    {
        SceneManager.LoadScene(1);
    }
    protected void ToShop(PointerEventData eventData)
    {
        system.OpenPage(shopPage);
    }
    protected void SetVibration(PointerEventData eventData)
    {
        GameInstance g = GameInstance.Instance;
        bool o = !g.GetVibration();
        g.SetVibration(o);
        if (o)
            vibrationButtomImage.sprite = vibrationOn;
        else
            vibrationButtomImage.sprite = vibrationOff;
    }
}
