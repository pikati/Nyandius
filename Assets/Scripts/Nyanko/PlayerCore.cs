public class PlayerCore
{
    public bool IsDead { get; private set; } = false;

    public void Dead()
    {
        IsDead = true;
    }
}
