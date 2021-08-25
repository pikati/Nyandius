using UniRx;
using UnityEngine;

//三毛猫コンポーネント
public class Mike : Character, IDamageApplicable
{
    private ICharacterAnimation _playerAnimation;
    private IPlayerInputEventProvider _playerInputEventPorvider;
    private Missiler _missiler;
    [SerializeField]
    private Option[] _options;
    private Barrier _barrier;
    private readonly int _optionMax = 2;
    private int _activOptionNum = 0;
    private readonly int _memoryPositionNum = 64;
    private Vector3[] _oldPosition;//オプションの座標で利用
    private ReactiveProperty<Vector3> _position = new ReactiveProperty<Vector3>();
    private bool _skipUpdateOldPosition = false;
    private SpriteRenderer _renderer;

    public bool IsActiveDoubler { get; set; } = false;

    private GameTimer _animTimer = new GameTimer(0.5f);
    private GameTimer _invicibleTimer = new GameTimer(0.5f);
    private void Start()
    {
        _oldPosition = new Vector3[_memoryPositionNum];
        _position.Value = transform.position;
        _position
            .Subscribe(_ => SetOldPosition())
            .AddTo(this);
        base.Initialize();
        _characterAttack = GetComponent<ICharacterAttack>();
        var a = transform.GetChild(0);
        _renderer = a.GetComponent<SpriteRenderer>();
        _playerAnimation = a.GetComponent<ICharacterAnimation>();
        _playerInputEventPorvider = GetComponent<IPlayerInputEventProvider>();
        _barrier = GetComponent<Barrier>();
        _missiler = Singleton<Missiler>.Instance;
        _missiler.ShooterNum = 1;
        _bulletType = BulletType.Normal;
    }

    protected override void UpdateFrame()
    {
        if (IsDead) return;
        if (_playerInputEventPorvider.OnShot.Value)
        {
            Attack();
        }
        if (_characterState != CharacterState.Move)
        {
            if (_animTimer.UpdateTimer())
            {
                ChangeCharacterState(CharacterState.Move, _playerAnimation);
                _animTimer.ResetTimer(0.5f);
            }
        }
        SetOldPosition();
        UpdateOptionPosition();
        _invicibleTimer.UpdateTimer();
    }

    protected override void Attack()
    {
        _characterAttack.Attack(transform.position, _bulletType);
        for (int i = 0; i < _activOptionNum; i++)
        {
            _options[i].Attack(false);
        }
        ShotMissile();
        ChangeCharacterState(CharacterState.Attack, _playerAnimation);
        _animTimer.ResetTimer(0.5f);
    }

    public override void OnCollision(Collider col)
    {
        if (IsDead) return;

        if (col.CompareTag("Enemy") || col.CompareTag("Ground"))
        {
            ApplyDamage();
        }
    }

    public void ApplyDamage(in int damage = 1)
    {
        if (!_invicibleTimer.IsTimeUp) return;
        _invicibleTimer.ResetTimer();
        if (_barrier.IsActiveBarrier())
        {
            _barrier.DamageBarrier(damage);
        }
        else
        {
            ChangeCharacterState(CharacterState.Damage, _playerAnimation);
            OnDamage(damage);
            _animTimer.ResetTimer(0.5f);
        }
        if (IsDead)
        {
            Singleton<CriSoundManager>.Instance.StopBGM();
            Singleton<CriSoundManager>.Instance.PlaySE(CueID.PlayerDead);
            //死亡エフェクト
            _renderer.enabled = false;
        }
    }

    public void ActivateOption()
    {
        if (_activOptionNum >= _optionMax) return;
        _options[_activOptionNum++].ActivateOption();
    }

    public void SetOptionBulletType(BulletType type)
    {
        _options[0].SetBulletType(type);
        _options[1].SetBulletType(type);
    }

    private void ShotMissile()
    {
        if (!_missiler.CanShotMissile) return;
        if (_missiler.ActiveMissiler)
        {
            _characterAttack.Attack(transform.position, BulletType.Missile);
            for (int i = 0; i < _activOptionNum; i++)
            {
                _options[i].Attack(true);
            }
        }
    }

    private void SetOldPosition()
    {
        if (_oldPosition[0] == transform.position) return;
        if(_skipUpdateOldPosition)
        {
            _skipUpdateOldPosition = false;
            return;
        }
        for (int i = _memoryPositionNum - 1; i > 0; i--)
        {
            _oldPosition[i] = _oldPosition[i - 1];
        }
        _oldPosition[0] = transform.position;
        _skipUpdateOldPosition = true;

    }

    private void UpdateOptionPosition()
    {
        _options[0].SetPosition(_oldPosition[_memoryPositionNum / 2]);
        _options[1].SetPosition(_oldPosition[_memoryPositionNum - 1]);
    }

    public void Restart()
    {
        Singleton<LifeManager>.Instance.ReduceLive();
        Reset();
    }

    public void Reset()
    {
        _hp.Value = 1;
        transform.position = new Vector3(-5, 0, 0);
        IsActiveDoubler = false;
        _activOptionNum = 0;
        _options[0].Reset();
        _options[1].Reset();
        _missiler.ShooterNum = 1;
        _missiler.ActiveMissiler = false;
        _renderer.enabled = true;
        _bulletType = BulletType.Normal;
    }

    public void ToStart()
    {
        Singleton<CriSoundManager>.Instance.PlayBGM(CueID.Intoro);
    }
}
