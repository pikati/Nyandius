using UniRx;

public class Character : Behaviour
{
    protected CharacterState _characterState = CharacterState.Move;
    protected BulletType _bulletType;
    protected IDamageApplicable _damageApplcable;
    protected ICharacterAttack _characterAttack;
    protected FloatReactiveProperty _hp = new FloatReactiveProperty(1);
    public bool IsDead
    {
        get
        {
            return _hp.Value <= 0;
        }
    }

    protected override void Initialize()
    {

    }

    protected override void UpdateFrame()
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

    protected void OnDamage(float damage)
    {
        _hp.Value -= damage;
    }

    protected virtual void OnDead()
    {

    }
}
