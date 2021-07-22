using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mike : Character
{
    private Shooter shooter = new Shooter();
    private GameTimer animTimer = new GameTimer(0.5f);
    protected override void Initialize()
    {
        base.Initialize();
    }

    protected override void UpdateFrame()
    {
        if(Singleton<InputController>.Instance.A)
        {
            Attack();
        }
        if(cState != CharacterState.Move)
        {
            if(animTimer.UpdateTimer())
            {
                ChangeCharacterState(CharacterState.Move);
                animTimer.ResetTimer(0.5f);
            }
        }
    }

    protected override void Attack()
    {
        shooter.ShotBullet(transform.position);
        ChangeCharacterState(CharacterState.Attack);
        animTimer.ResetTimer(0.5f);
    }

    protected override void Damage()
    {
        ChangeCharacterState(CharacterState.Damage);
        animTimer.ResetTimer(0.5f);
    }

    public override void OnCollision(Collider col)
    {
        if(col.CompareTag("Enemy") || col.CompareTag("Ground"))
        {
            Damage();
        }
    }


}
