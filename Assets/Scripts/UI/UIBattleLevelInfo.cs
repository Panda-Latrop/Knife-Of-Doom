using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBattleLevelInfo : UIPage
{
    [SerializeField]
    protected Text score, stage;
    [SerializeField]
    protected string stageName = "ENEMY ", bossStageName = "BOSS";
    [SerializeField]
    protected Color stageColor, bossStageColor;
    public void SetStageNameVisability(bool visable)
    {
        stage.enabled = visable;
    }
    public void SetStage(int stage)
    {
        this.stage.text = stageName + stage.ToString();
        this.stage.color = stageColor;
    }
    public void SetBossStage()
    {
        this.stage.text = bossStageName;
        this.stage.color = bossStageColor;
    }
    public void SetBossStage(string name)
    {
        this.stage.text = name;
        this.stage.color = bossStageColor;
    }
    public void SetScore(int score)
    {
        this.score.text = score.ToString();
    }
}
