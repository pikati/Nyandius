using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//三毛猫コンポーネント
public class Mike : Character, IDamageApplicable
{
    private PlayerCore _playerCore = new PlayerCore();
    private ICharacterAnimation _playerAnimation;
    private IPlayerInputEventProvider _playerInputEventPorvider;
    private Missiler _missiler;
    private readonly int _shooterMax = 4;
    private Vector3[] _shooterPosition;
    public bool IsActiveDoubler { get; set; } = false;
    
    private GameTimer _animTimer = new GameTimer(0.5f);
    protected override void Initialize()
    {
        _hp = 1;
        base.Initialize();
        _characterAttack = GetComponent<ICharacterAttack>();
        var a = transform.GetChild(0);
        _playerAnimation = a.GetComponent<ICharacterAnimation>();
        _playerInputEventPorvider = GetComponent<IPlayerInputEventProvider>();
        _missiler = Singleton<Missiler>.Instance;
        _missiler.ShooterNum++;
        _bulletType = BulletType.Normal;
        _shooterPosition = new Vector3[_shooterMax];
        for(int i = 0; i < _shooterMax; i++)
        {
            _shooterPosition[i].z = -1;
        }
    }

    protected override void UpdateFrame()
    {
        if(_playerInputEventPorvider.OnShot.Value)
        {
            Attack();
        }
        if(_characterState != CharacterState.Move)
        {
            if(_animTimer.UpdateTimer())
            {
                ChangeCharacterState(CharacterState.Move, _playerAnimation);
                _animTimer.ResetTimer(0.5f);
            }
        }
        UpdateShooterPosition();
    }

    protected override void Attack()
    {
        _characterAttack.Attack(transform.position, _bulletType);
        if(IsActiveDoubler)
        {
            _characterAttack.Attack(transform.position + new Vector3(0, 0.4f, 0), _bulletType);
        }
        for (int i = 0; i < _shooterMax; i++)
        {
            if(_shooterPosition[i].z != -1 && _missiler.CanShotMissile)
            {
                _characterAttack.Attack(new Vector2(_shooterPosition[i].x, _shooterPosition[i].y), BulletType.Missile);
            }
        }
        ChangeCharacterState(CharacterState.Attack, _playerAnimation);
        _animTimer.ResetTimer(0.5f);
    }

    protected override void Damage()
    {
        ChangeCharacterState(CharacterState.Damage, _playerAnimation);
        _animTimer.ResetTimer(0.5f);
    }

    public override void OnCollision(Collider col)
    {
        if (col.CompareTag("Enemy") || col.CompareTag("Ground"))
        {
            Damage();
        }
    }

    public void ApplyDamage(in int damage)
    {
        //バリアあるとき
        //バリアクラスのhp減らす
        //バリアないとき
        _hp -= damage;
        
        if(IsDead)
        {
            //死亡エフェクト
            DestroyThis();
        }
    }

    private void UpdateShooterPosition()
    {
        _shooterPosition[0] = transform.position;
    }
}
