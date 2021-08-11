using System;
using UniRx;
using UnityEngine;

public class PowerUpPresenter : MonoBehaviour
{
    [SerializeField]
    private PowerUpModel _model;
    [SerializeField]
    private PowerUpView _view;

    private void Start()
    {
        _model.PowerUpNum
            .Subscribe(x => { _view.SetSelectColor(x); })
            .AddTo(this);
    }
}
