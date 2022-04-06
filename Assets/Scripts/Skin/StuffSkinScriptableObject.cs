using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Skin", menuName = "Game/Skin", order = 2)]
public class StuffSkinScriptableObject : ScriptableObject
{
    [SerializeField]
    protected Sprite[] sprites;
    [SerializeField]
    protected Color color = Color.white;
    [SerializeField]
    protected string description;
    [SerializeField]
    protected int group;
    [SerializeField]
    protected bool byBoss,open;

    public Sprite[] Sprites => sprites;
    public Color Color => color;
    public string Description => description;
    public bool ByBoss => byBoss;
    public bool Open => open;

}
public class StuffSkinInfo
{
    public StuffSkinScriptableObject so;
    public bool open;

    public StuffSkinInfo(StuffSkinScriptableObject so, bool open)
    {
        this.so = so;
        this.open = open;
    }
}