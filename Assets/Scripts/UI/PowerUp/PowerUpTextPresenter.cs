using UniRx;
using UnityEngine;

public class PowerUpTextPresenter : MonoBehaviour
{
    [SerializeField]
    private PowerUpTextModel _model;
    [SerializeField]
    private PowerUpTextView _view;

    // Start is called before the first frame update
    void Start()
    {
        _model.CanSpeedUp
            .Subscribe(x => _view.DispText(0, x))
            .AddTo(this);
        _model.CanMissile
            .Subscribe(x => _view.DispText(1, x))
            .AddTo(this);
        _model.CanDouble
            .Subscribe(x => _view.DispText(2, x))
            .AddTo(this);
        _model.CanLazer
            .Subscribe(x => _view.DispText(3, x))
            .AddTo(this);
        _model.CanOption
            .Subscribe(x => _view.DispText(4, x))
            .AddTo(this);
    }
}
