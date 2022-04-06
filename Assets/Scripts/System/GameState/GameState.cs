using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class GameState : Actor
{

    protected LevelLoader levelLoader = new LevelLoader();
    
    [SerializeField]
    protected PlayerController playerController;
    [SerializeField]
    protected new CameraActor camera;
    [SerializeField]
    protected MusicHolder musicHolder;

    protected bool gameWin, gameOver;
    [SerializeField]
    protected float gamePause = 2.0f;

  

    protected float nextPause;

    //[SerializeField]
    protected LevelSettingScriptableObject levelSetting;
    protected int stage = 1, localStage = 1;
    protected bool bossStage;
    protected int hitCount;

    [SerializeField]
    protected Vector3 logStartPosition;
    protected LogActor log;
    [SerializeField]
    protected float knifeFireRate = 2.0f, knifeSpeed = 20.0f;

    [SerializeField]
    protected StuffSkinScriptableObject knifeSkin;




    [SerializeField]
    protected KnifeActor knifePrefab,logKnifePrefab;
    [SerializeField]
    protected AppleActor applePrefab;
    [SerializeField]
    protected LogActor logPrefab;
    [SerializeField]
    protected DynamicActor logAppearAffect;

    [SerializeField]
    protected KnifeThrowerComponent knifeThrower;

    [SerializeField]
    protected ParalaxActor paralax;
    [SerializeField]
    protected HeroActor hero;

    [SerializeField]
    protected UISystem uiSystem;
    [SerializeField]
    protected UIRewardTab rewardTab;
    [SerializeField]
    protected UIHitCountTab hitCountTab;
    [SerializeField]
    protected UIBattleLevelInfo battleLevelTab;
    [SerializeField]
    protected UIGameOverPage gameOverPage;
    [SerializeField]
    protected AudioSource gameOverSource;

    public PlayerController PlayerController => playerController;
    public CameraActor Camera => camera.Setup();
    public MusicHolder MusicHolder => musicHolder;
    public LevelLoader LevelLoader => levelLoader;

    protected override void OnAwake()
    {

        knifeThrower.SetKnife(knifePrefab);
        knifeThrower.SetSpeed(knifeSpeed);
        knifeThrower.SetFireRate(knifeFireRate);
    }

    protected override void OnStart()
    {
        battleLevelTab.SetScore(GameInstance.Instance.GetScore());
        ToFirtsStage();
    }
    public void ToFirtsStage()
    {
        paralax.enabled = true;
        paralax.SetDefault();
        hero.PlayRun();
        stage = 1;
        localStage = 1;
        bossStage = false;
        levelLoader.TryLoadLevel(stage, ref levelSetting);
        CreateLog();
    }
    public void SetKnifeSkin(StuffSkinScriptableObject skin)
    {
        knifeSkin = skin;
        knifeThrower.SetSkin(knifeSkin);
    }
    public StuffSkinScriptableObject GetKnifeSkin() => knifeSkin;
    public void SetKnifeSkin(string skin)
    {
       SetKnifeSkin(levelLoader.LoadKnifeSkin(skin));

    }
    public void NextStage()
    {
        stage++;
        if (bossStage)
        {
            localStage = 1;
            bossStage = false;
        }
        else
            localStage++;



        if (localStage == 5)
        {
            levelSetting = levelLoader.LoadRandomBossLevel();
            bossStage = true;
        }
        else
        {
            if (levelLoader.TryLoadLevel(stage, ref levelSetting))
            {
                Debug.Log("Success Load Level Setting: " + levelSetting.name);
            }
            else
            {
                Debug.Log("Fail Load Level Setting ID: " + stage);
            }
        }

        CreateLog();



    }

    [ContextMenu("Create Log")]
    public void CreateLog()
    {
        PoolManager pool = GameInstance.Instance.PoolManager;
        log = pool.Pop(logPrefab) as LogActor;
        log.SetPosition(logStartPosition);
        log.SetSkin(levelSetting.LogSkin);
        log.SetCurve(levelSetting.LogRotationCurve);
        int knifesInLog = Random.Range(0, levelSetting.MaxKnifesInLog + 1);
        int appleInLog = Random.Range(0, levelSetting.MaxAppleInLog + 1);
        float step = 360.0f / (knifesInLog + appleInLog + 2);
        for (int i = 0; i < knifesInLog; i++)
        {
            KnifeActor knife = pool.Pop(logKnifePrefab) as KnifeActor;
            knife.Stuck();
            knife.transform.position = log.transform.position + Quaternion.AngleAxis(Random.Range(0.75f,1.0f)*step * (i+1), Vector3.forward) * Vector3.up;
            knife.transform.rotation = Quaternion.LookRotation(Vector3.forward, (log.transform.position - knife.transform.position).normalized);
            log.AttachStuff(knife);
        }
        for (int i = 0; i < appleInLog; i++)
        {
            if (Random.Range(0, 1.0f) <= levelSetting.AppleAppearChance)
            {
                AppleActor apple = pool.Pop(applePrefab) as AppleActor;
                apple.transform.position = log.transform.position + Quaternion.AngleAxis(Random.Range(0.75f, 1.0f) *step * (knifesInLog + i + 1), Vector3.forward) * Vector3.up;
                apple.transform.rotation = Quaternion.LookRotation(Vector3.forward, (apple.transform.position - log.transform.position).normalized);
                log.AttachStuff(apple, apple.Bounds.extents.y * 0.9f);
            }
        }
        pool.Pop(logAppearAffect).SetPosition(log.transform.position);
        hitCountTab.SetMaxHit(levelSetting.MaxHits);


        battleLevelTab.SetStage(stage);
        battleLevelTab.SetStageNameVisability(true);
        if (bossStage)
            battleLevelTab.SetBossStage(levelSetting.LogSkin.Description);

        //log.PlayAppear();
    }
    public void ThrowKnife()
    {
        if(!gameOver && !gameWin)
        knifeThrower.Shoot(knifeThrower.ShootPoint.position, Vector3.up);
    }
    public void OnHitApple(AppleActor what,GameStuffActor by)
    {
        log.RemoveStuff(what);
        battleLevelTab.SetScore(GameInstance.Instance.AddScore(1));
        Debug.Log("Hit Apple");
       // Debug.Break();
    }
    public void OnHitLog(LogActor what,GameStuffActor by)
    {
        log.AttachStuff(by);
        hitCountTab.SetHit(hitCount);
        hitCount++;
        if (hitCount >= levelSetting.MaxHits)
            GameWin();
        else
        {
            camera.PlaySmallShake();
             Vibrate();
        }
    }

    protected void Vibrate()
    {
        if(GameInstance.Instance.GetVibration())
        Handheld.Vibrate();
    }

    public void OnHitKnife(KnifeActor what,GameStuffActor by)
    {
        GameOver();
    }

    public void GameWin() 
    {
        GameInstance.Instance.SaveData();
        battleLevelTab.SetStageNameVisability(false);
        camera.PlayShake();
         Vibrate(); 
        log.Break();
        Reward();
        hitCount = 0;
        nextPause = Time.time + gamePause;
        gameWin = true;
        enabled = true;
        Debug.Log("Game Win");
    }
    public void Reward()
    {
        if (levelSetting.IsBoss)
        {
            StuffSkinScriptableObject reward = levelSetting.BossReward;
            if (GameInstance.Instance.AddSkin(reward)){
                rewardTab.SetIcon(reward);
                rewardTab.PlayAnimation();
            }
        }
    }
    public void Restart()
    {
        GameInstance.Instance.PoolManager.Push(log);
        ToFirtsStage();
        gameWin = gameOver = false;
    }
    public void GameOver() 
    {
        GameInstance.Instance.SetMaxStage(stage);
        GameInstance.Instance.SaveData();
        camera.PlayShake();
        hero.PlayDie();
        paralax.enabled = false;
        enabled = true;
         Vibrate();
        hitCount = 0;       
        log.enabled = false;
        nextPause = Time.time + gamePause;
        gameOver = true;
        gameOverSource.Play();
        Debug.Log("Game Over");
    }
    protected override void OnLateUpdate()
    {
        if (gameOver || gameWin)
        {
            if (Time.time >= nextPause)
            {
                if (gameOver)
                {
                    //GameInstance.Instance.PoolManager.Push(log);
                    //ToFirtsStage();
                    uiSystem.OpenPage(gameOverPage);
                    enabled = false;
                }
                else
                {
                    NextStage();
                    gameWin = gameOver = false;
                }
                
            }
        }
    }
    private void Awake()
    {
        OnAwake();
    }
    private void Start()
    {
        OnStart();
    }
    private void LateUpdate()
    {
        OnLateUpdate();
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(logStartPosition, 0.5f);
    }

}
