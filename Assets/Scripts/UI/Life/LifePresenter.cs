using UniRx;
using UnityEngine;

public class LifePresenter : MonoBehaviour
{
    [SerializeField]
    private LifeManager _lifeManager;
    [SerializeField]
    private LifeView _view;
    // Start is called before the first frame update
    void Start()
    {
        _lifeManager.Life
            .Subscribe(x => _view.UpdateText(x))
            .AddTo(this);
    }
}
