using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public sealed class PlayerInputEventImpl : Behaviour, IPlayerInputEventProvider
{

    #region InputEventProvider
    public IReadOnlyReactiveProperty<bool> OnShot => _shotSubject;
    public IReadOnlyReactiveProperty<bool> OnPowerUp => _powerUpSubject;
    public IReadOnlyReactiveProperty<Vector2> MoveDirection => _move;
    #endregion

    private readonly ReactiveProperty<bool> _shotSubject = new ReactiveProperty<bool>();
    private readonly ReactiveProperty<bool> _powerUpSubject = new ReactiveProperty<bool>();
    private readonly ReactiveProperty<Vector2> _move = new ReactiveProperty<Vector2>();
    // Start is called before the first frame update

    protected override void Initialize()
    {
        _shotSubject.AddTo(this);
        _powerUpSubject.AddTo(this);
        _move.AddTo(this);
    }

    protected override void UpdateFrame()
    {
        var input = Singleton<InputController>.Instance;
        _shotSubject.Value = input.A;
        _powerUpSubject.Value = input.Y;

        if (input.MoveValue.magnitude != 0)
        {
            _move.SetValueAndForceNotify(input.MoveValue);
        }
        else
        {
            _move.SetValueAndForceNotify(new Vector2(-input.Left + input.Right, input.Up + -input.Down));
        }
    }
}
