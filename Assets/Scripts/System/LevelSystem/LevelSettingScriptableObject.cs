using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "lvl ", menuName = "System/Level Setting", order = 2)]
public class LevelSettingScriptableObject : ScriptableObject
{
    [Header("File Must Be Named By Pattern \"lvl *\" where \"*\" Stage Number")]
    [SerializeField]
    protected StuffSkinScriptableObject logSkin;
    [SerializeField]
    protected int maxHits = 5,maxKnifesInLog = 3,maxAppleInLog = 2;
    [SerializeField]
    protected float appleAppearChance = 0.25f;
    [SerializeField]
    protected AnimationCurve logRotationCurve;
    [Header("Boss")]
    [SerializeField]
    protected bool isBoss = false;
    [SerializeField]
    protected StuffSkinScriptableObject bossReward;
    public bool IsBoss => isBoss;
    public StuffSkinScriptableObject LogSkin => logSkin;
    public int MaxHits => maxHits;
    public int MaxKnifesInLog => maxKnifesInLog;
    public int MaxAppleInLog => maxAppleInLog;
    public float AppleAppearChance => appleAppearChance;
    public AnimationCurve LogRotationCurve => logRotationCurve;
    public StuffSkinScriptableObject BossReward => bossReward;
}
