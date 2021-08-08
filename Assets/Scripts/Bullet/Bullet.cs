public abstract class Bullet : Behaviour
{
    protected Bullet()
    {

    }
    protected float _speed = 0.1f;
    protected bool _canPierce = false;

    protected override void UpdateFrame()
    {
        Move();
    }

    protected virtual void OnBecameInvisible()
    {
        DestroyThis();
    }

    protected abstract void Move();
}
