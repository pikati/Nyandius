using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Behaviour
{
    public enum CharacterState
    {
        Move,
        Attack,
        Damage
    };

    protected CharacterState cState = CharacterState.Move;
    public CharacterState CState => cState;
    protected override void Initialize()
    {

    }

    protected override void UpdateFrame()
    {

    }

    protected virtual void Damage()
    {

    }

    protected virtual void Attack()
    {

    }

    protected virtual void ChangeCharacterState(CharacterState state)
    {
        cState = state;
    }
}
