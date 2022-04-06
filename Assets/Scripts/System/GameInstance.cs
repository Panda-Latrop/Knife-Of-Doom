using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using SimpleJSON;

public  class GameInstance : Singleton<GameInstance>
{
    protected static bool changeScene;
    public static string language = "eng";
    public static bool ChangeScene => changeScene;

    //protected SaveSystem saveSystem = new SaveSystem();
    protected SimpleSaveSystem saveSystem = new SimpleSaveSystem();
    protected PoolManager poolManager;
    protected GameState gameState;
    protected PlayerController playerController;
    protected MusicSystem musicSystem;

    protected string level;
    protected int levelEnter = 0;
    protected int levelLoadState = NONE;
    protected const int NONE = 0, LOAD = 1, NEXT = 2;
    public GameState GameState  => gameState; 
    public PlayerController PlayerController => playerController; 
    public PoolManager PoolManager  => poolManager;
    public MusicSystem MusicSystem => musicSystem;

    protected override void Awake()
    {
        bool bindOnSceneLoaded = (Instance == null);
        base.Awake();
        if (bindOnSceneLoaded)
        {
            {
                SceneManager.sceneLoaded += OnSceneLoaded;
                saveSystem.Load();
            }
        }
    }
    protected void Create()
    {     
        GameObject gogs = GameObject.FindGameObjectWithTag("GameController");
        if (gogs != null)
        {
            gameState = gogs.GetComponent<GameState>();

            string knifeSkinName = saveSystem.Data.currentSkin;
            if (knifeSkinName.Length > 0)
                gameState.SetKnifeSkin(knifeSkinName);
            else
                saveSystem.Data.currentSkin = gameState.GetKnifeSkin().name;




            playerController = gameState.PlayerController;
            playerController.enabled = true;
            playerController.SetCamera(gameState.Camera);
            playerController.SetGameState(gameState);
            if (musicSystem == null)
            {
                GameObject goms = new GameObject("MusicSystem");
                musicSystem = goms.AddComponent(typeof(MusicSystem)) as MusicSystem;
                AudioSource audio = goms.AddComponent(typeof(AudioSource)) as AudioSource;
                musicSystem.SetAudioSource(audio);
                musicSystem.SetMusicHolder(gameState.MusicHolder);
                DontDestroyOnLoad(goms);
                goms.transform.SetParent(transform);
            }
            else
            {
                musicSystem.enabled = true;
                musicSystem.SetMusicHolder(gameState.MusicHolder);
            }
            
        }
        else
        {
            if (gogs == null)
                Debug.Log("GameState Not Found");
            if (musicSystem != null)
            {
                musicSystem.enabled = false;
                musicSystem.Stop();
            }
                Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            return;
        }
        GameObject gopm = new GameObject("PoolManager");
        poolManager = gopm.AddComponent(typeof(PoolManager)) as PoolManager;
        gopm.transform.SetParent(gogs.transform);
    }
    [ContextMenu("Log Data")]
    protected void LogData()
    {
        Debug.Log(saveSystem.Data.ToString());
    }
    public void SetMaxStage(int stage)
    {
        saveSystem.Data.maxStage = Mathf.Max(saveSystem.Data.maxStage, stage);
    }
    public string GetCurrentSkinName() => saveSystem.Data.currentSkin;
    public void SetCurrentSkinName(string value) => saveSystem.Data.currentSkin = value;
    public int GetMaxStage() => saveSystem.Data.maxStage;
    public int GetScore() => saveSystem.Data.score;
    public int AddScore(int value) { return (saveSystem.Data.score += value); }
    public bool GetVibration() => saveSystem.Data.vibrate;
    public void SetVibration(bool value) => saveSystem.Data.vibrate = value;
    public bool TransactionScore(int price)
    {
        if(saveSystem.Data.score >= price)
        {
            saveSystem.Data.score -= price;
            return true;
        }
        return false;
    }
    public bool AddSkin(StuffSkinScriptableObject skin)
    {
        var skins = saveSystem.Data.skins;
        if (skins.Contains(skin.name))
        {
            return false;
        }
        else
        {
            skins.Add(skin.name);
            return true;
        }
    }
    public bool HasSkin(StuffSkinScriptableObject skin)
    {
        return saveSystem.Data.skins.Contains(skin.name);
    }
    public void SaveData()
    {
        saveSystem.Save();
    }

    //[ContextMenu("Save")]
    //public void InitiateSaveGame()
    //{
    //    saveSystem.Data.save = level;
    //    saveSystem.Prepare(false).
    //               SaveTo("system", true, "system").
    //               SaveTo(level).
    //               DataTo("save.sv").
    //               Close();
    //}
    //[ContextMenu("Load")]
    //public void InitiateLoadGame()
    //{
    //    saveSystem.DataFrom("save.sv");
    //    LoadScene(saveSystem.Data.save, 0, LOAD);

    //}
    public bool GetLoadedObject(string name, ref GameObject go)
    {
        //return saveSystem.GetLoadedObject(name, ref go);
        return false;
    }
    //public void LoadScene(int enter, int state = NEXT)
    //{
    //    LoadScene(level, enter, state);
    //}
    //public void LoadScene(string name,int enter, int state = NEXT)
    //{
    //    changeScene = true;
    //    levelLoadState = state;
    //    levelEnter = enter;

    //    switch (levelLoadState)
    //    {
    //        case NONE:
    //            saveSystem.ClearData();
    //            break;
    //        case LOAD:
    //            break;
    //        case NEXT:
    //            saveSystem.Prepare(false).
    //                       SaveTo(level, true, "between").
    //                       SaveTo("next",true,"next").
    //                       Close();
    //            break;
    //        default:
    //            break;
    //    }       
    //    SceneManager.LoadScene(name.ToLower(), LoadSceneMode.Single);
    //}
    //void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    //{
    //    level = scene.name.ToLower();
    //    changeScene = false;
    //    Create();
    //    switch (levelLoadState)
    //    {
    //        case NONE:
    //            saveSystem.ClearData();
    //            break;
    //        case LOAD:
    //            saveSystem.Prepare(true).
    //                       LoadFrom("system", true, "system").
    //                       LoadFrom(level).
    //                       Close();
    //            break;
    //        case NEXT:
    //            saveSystem.Prepare(true).
    //                       LoadFrom(level, true,"between").
    //                       LoadFrom("next", true, "next").
    //                       Close();
    //            break;
    //        default:
    //            break;
    //    }
    //}
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        level = scene.name.ToLower();
        changeScene = false;
        Create();
    }
    private void OnApplicationQuit()
    {
        SaveData();
    }
}
