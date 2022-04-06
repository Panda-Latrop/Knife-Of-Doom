using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader
{
    protected LevelSettingScriptableObject levelSetting;
    protected const string levelsFloder = "Stages/Level/";
    protected const string bossesFloder = "Stages/Boss/";
    protected const string skinsFloder = "Skins/Knifes/";
    public bool TryLoadLevel(int level, ref LevelSettingScriptableObject levelSetting)
    {
        //Debug.Log(levelsFloder + level.ToString());
        levelSetting = Resources.Load<LevelSettingScriptableObject>(levelsFloder+ "lvl " + level.ToString());
        if(levelSetting != null)
        {
            Resources.UnloadAsset(this.levelSetting);
            this.levelSetting = levelSetting;
            return true;
        }
        else
        {
            levelSetting = this.levelSetting;
            return false;
        }
    }
    public LevelSettingScriptableObject LoadRandomBossLevel()
    {
        var bosses = Resources.LoadAll(bossesFloder);
        int rand = Random.Range(0, bosses.Length);
        return bosses[rand] as LevelSettingScriptableObject;        
    }
    public StuffSkinScriptableObject LoadKnifeSkin(string name)
    {
        return Resources.Load<StuffSkinScriptableObject>(skinsFloder + name);
    }
    public StuffSkinScriptableObject[] LoadAllKnifeSkins()
    {
        return Resources.LoadAll<StuffSkinScriptableObject>(skinsFloder);
    }
}
