using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class Churu : Behaviour
{
    private Character _mike;
    protected override void Initialize()
    {
        _mike = GameObject.FindGameObjectWithTag("Player").GetComponent<Mike>();
        base.Initialize();
    }

    protected override void UpdateFrame()
    {
        transform.position += new Vector3(-1.5f, 0, 0) * Time.deltaTime;
        if(_mike.IsDead)
        {
            DestroyThis();
        }
    }
    public override void OnCollision(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            Singleton<CriSoundManager>.Instance.PlaySE(CueID.PowerUp);
            Singleton<GameManager>.Instance.PowerUpManager.GetPowerUp();
            DestroyThis();
        }
    }

    private void OnBecameInvisible()
    {
        DestroyThis();
    }
}
