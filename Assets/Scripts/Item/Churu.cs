using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class Churu : Behaviour
{
    protected override void Initialize()
    {
        this.UpdateAsObservable()
            .Subscribe(_ => transform.position += new Vector3(-1.0f, 0, 0) * Time.deltaTime);
        base.Initialize();
    }
    public override void OnCollision(Collider col)
    {
        if(col.CompareTag("Player"))
        {
            Singleton<GameManager>.Instance.PowerUpManager.GetPowerUp();
            DestroyThis();
        }
    }

    private void OnBecameInvisible()
    {
        DestroyThis();
    }
}
