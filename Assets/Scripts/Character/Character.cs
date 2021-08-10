using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Behaviour
{
    protected CharacterState _characterState = CharacterState.Move;
    protected BulletType _bulletType;
    protected IDamageApplicable _damageApplcable;
    protected ICharacterAttack _characterAttack;
    protected int _hp = -1;
    public bool IsDead
    {
        get
        {
            return _hp <= 0 ? true : false;
        }
    }

    protected override void Initialize()
    {

    }

    protected override void UpdateFrame()
    {

    }

    protected virtual void Damage()
    {

    }

    protected virtual void Attack()
    {

    }

    protected virtual void ChangeCharacterState(CharacterState state, ICharacterAnimation animation)
    {
        if (state == _characterState) return;
        _characterState = state;
        animation.ChangeAnimation(state);
    }

    public virtual void ChangeBulletType(BulletType type)
    {
        _bulletType = type;
    }
}
