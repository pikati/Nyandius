public class PlayerCore
{
    public bool IsDead { get; private set; } = false;
    private bool _isInvincible = false;

    public void OnCollision()
    {
        if (_isInvincible) return;
        IsDead = true;
    }

    public void SetInvicivle(bool isInvicible)
    {
        _isInvincible = isInvicible;
    }
}
