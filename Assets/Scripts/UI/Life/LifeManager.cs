using UniRx;
using UnityEngine;

public class LifeManager : Singleton<LifeManager>
{
    private IntReactiveProperty _life = new IntReactiveProperty(3);
    public IReadOnlyReactiveProperty<int> Life => _life;
    private void Start()
    {
        _life
            .Where(x => x < 0)
            .Subscribe(_ => EndMainGame())
            .AddTo(this);
    }

    public void ReduceLive()
    {
        _life.Value--;
    }

    public void AddLive()
    {
        _life.Value++;
    }

    private void EndMainGame()
    {
        Singleton<GameFacilitator>.Instance.DispResult();
    }

}
