using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public enum CharacterState
    {
        Move,
        Attack,
        Damage
    };

    protected CharacterState cState = CharacterState.Move;
    public CharacterState CState => cState;
    protected Collider myCollider;
    private void Start()
    {
        myCollider = GetComponent<Collider>();
        Initialize();
    }

    private void Update()
    {
        UpdateFrame();
    }
    protected virtual void Initialize()
    {

    }

    protected virtual void UpdateFrame()
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

    public virtual void OnCollision(Collider col)
    {

    }
}
