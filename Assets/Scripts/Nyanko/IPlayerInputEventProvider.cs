using System;
using UniRx;
using UnityEngine;

public interface IPlayerInputEventProvider
{
    IReadOnlyReactiveProperty<Vector2> MoveDirection { get; }
    IReadOnlyReactiveProperty<bool> OnShot { get; }
    IReadOnlyReactiveProperty<bool> OnPowerUp { get; }

}
