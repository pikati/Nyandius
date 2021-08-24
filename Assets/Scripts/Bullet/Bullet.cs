public abstract class Bullet : Behaviour
{
    protected Bullet()
    {

    }
    protected float _speed = 0.1f;
    protected bool _canPierce = false;
    protected float _damage = 1;

    protected override void UpdateFrame()
    {
        Move();
        if (Singleton<GameManager>.Instance.IsPlayerDead())
        {
            DestroyThis();
        }
    }

    protected virtual void OnBecameInvisible()
    {
        DestroyThis();
    }

    public float Damage()
    {
        return _damage;
    }

    protected abstract void Move();
}
