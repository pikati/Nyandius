using System;
using UniRx;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public PowerUpManager PowerUpManager { get; private set; }
    private ColliderManager _colliderManager;
    private EnemyCreater _enemyCreater;
    [SerializeField]
    private Mike _player;
    private BoolReactiveProperty _isDead = new BoolReactiveProperty(false);
    private GameTimer _resetTimer = new GameTimer(3.0f);
    private bool _endGame = false;
    private GameFacilitator _gameFicillitator;
    public int LoopNum { get; private set; } = 2;
    public GameTimer BossTimer { get; private set; } = new GameTimer(0);
    private bool _isMiddleBoss = true;
    public bool IsBoss = false;

    // Start is called before the first frame update
    void Awake()
    {
        _colliderManager = new ColliderManager();
        PowerUpManager = new PowerUpManager();
        PowerUpManager.SetPowerUpModel(GameObject.Find("PowerUpModel").GetComponent<PowerUpModel>());
        _enemyCreater = GameObject.Find("EnemyPopManager").GetComponent<EnemyCreater>();
        _gameFicillitator = Singleton<GameFacilitator>.Instance;
        _isDead
            .Where(x => x == true)
            .Subscribe(_ => ResetGame())
            .AddTo(this);
    }

    private void Start()
    {
        PowerUpManager.Initialize();
        PowerUpManager.SetPlayer(_player);
        _endGame = false;
        StartGame();
        Singleton<CriSoundManager>.Instance.PlayBGM(CueID.Intoro);
}

    // Update is called once per frame
    void Update()
    {
        if (_gameFicillitator.GetGameState() != GameStateController.GameStateEnum.Game) return;
        _colliderManager.UpdateCollision();
        PowerUpManager.Update();
        _isDead.Value = _player.IsDead;
        if(_endGame)
        {
            if(_resetTimer.UpdateTimer())
            {
                RestartGame();
            }
        }
        BossTimer.UpdateTimer();
    }

    public void RegisterCollider(CircleCollider c)
    {
        _colliderManager.RegisterCollider(c);
    }

    public void RegisterCollider(RectCollider r)
    {
        _colliderManager.RegisterCollider(r);
    }

    public ColliderManager GetColliderManager()
    {
        return _colliderManager;
    }

    public bool IsPlayerDead()
    {
        return _player.IsDead;
    }

    public void SetBossTimer()
    {
        Invoke("ResetBossTimer", 4.0f);
    }

    public void CountLoop()
    {
        IsBoss = false;
        LoopNum++;
        _player.ToStart();
    }

    private void OnDestroy()
    {
        PowerUpManager.Destory();
    }

    private void ResetGame()
    {
        _endGame = true;
    }

    public void StartGame()
    {
        _player.Reset();
        _enemyCreater.Reset();
        Singleton<CriSoundManager>.Instance.PlayBGM(CueID.Intoro);
    }

    private void RestartGame()
    {
        _endGame = false;
        _enemyCreater.Reset();
        _player.Restart();
        PowerUpManager.Reset();
        _resetTimer.ResetTimer();
        Singleton<CriSoundManager>.Instance.PlayBGM(CueID.Intoro);
        Singleton<ScoreManager>.Instance.StageScoreZero();
    }

    //Invoke
    public void ResetBossTimer()
    {
        if(_isMiddleBoss)
        {
            BossTimer.ResetTimer(20.0f + (LoopNum * 10.0f));
        }
        else
        {
            IsBoss = true;
        }
        _isMiddleBoss = !_isMiddleBoss;
    }

    public void GameClear()
    {
        _endGame = false;
        _player.Restart();
        PowerUpManager.Reset();
        _resetTimer.ResetTimer();
        ResetLoopNum();
    }

    public void ResetLoopNum()
    {
        LoopNum = 0;
    }
}
