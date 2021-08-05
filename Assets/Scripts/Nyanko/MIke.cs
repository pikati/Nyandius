using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//三毛猫コンポーネント
public class Mike : Character, IDamageApplicable
{
    private PlayerCore _playerCore = new PlayerCore();
    private ICharacterAnimation _playerAnimation;
    private IPlayerInputEventProvider _playerInputEventPorvider;
    
    private GameTimer _animTimer = new GameTimer(0.5f);
    protected override void Initialize()
    {
        base.Initialize();
        _characterAttack = GetComponent<ICharacterAttack>();
        var a = transform.GetChild(0);
        _playerAnimation = a.GetComponent<ICharacterAnimation>();
        _playerInputEventPorvider = GetComponent<IPlayerInputEventProvider>();
        _bulletType = BulletType.Normal;
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
    }

    protected override void Attack()
    {
        _characterAttack.Attack(transform.position, _bulletType);
        ChangeCharacterState(CharacterState.Attack, _playerAnimation);
        _animTimer.ResetTimer(0.5f);
    }

    protected override void Damage()
    {
        ChangeCharacterState(CharacterState.Damage, _playerAnimation);
        _animTimer.ResetTimer(0.5f);
    }

    //public override void OnCollision(Collider col)
    //{
    //    if(col.CompareTag("Enemy") || col.CompareTag("Ground"))
    //    {
    //        Damage();
    //    }
    //}

    public void ApplyDamage(in int damage)
    {
        //バリアあるとき

        //バリアないとき
        DestroyThis();
        //死亡エフェクト

    }
}
