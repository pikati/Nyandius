using System;
using UniRx;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public PowerUpManager PowerUpManager { get; private set; }
    private ColliderManager _colliderManager;
    private EnemyCreater _enemyCreater;
    private Mike _player;
    private BoolReactiveProperty _isDead = new BoolReactiveProperty(false);
    private GameTimer _resetTimer = new GameTimer(3.0f);
    private bool _endGame = false;
    private GameFacilitator _gameFicillitator;

    // Start is called before the first frame update
    void Awake()
    {
        _colliderManager = new ColliderManager();
        PowerUpManager = new PowerUpManager();
        PowerUpManager.SetPowerUpModel(GameObject.Find("PowerUpModel").GetComponent<PowerUpModel>());
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Mike>();
        _enemyCreater = GameObject.Find("EnemyPopManager").GetComponent<EnemyCreater>();
        _gameFicillitator = Singleton<GameFacilitator>.Instance;
        _isDead
            .Where(x => x == true)
            .Subscribe(_ => ResetGame())
            .AddTo(this);
    }

    private void Start()
    {
        ResetGame();
        RestartGame();
        PowerUpManager.SetPlayer(_player);
        _endGame = false;
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

    private void OnDestroy()
    {
        PowerUpManager.Destory();
    }

    private void ResetGame()
    {
        _enemyCreater.Reset();
        _endGame = true;
    }

    private void RestartGame()
    {
        _endGame = false;
        _player.Restart();
        _resetTimer.ResetTimer();
    }
}
