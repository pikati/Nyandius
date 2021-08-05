using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour, ICharacterAnimation
{
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void ChangeAnimation(CharacterState state)
    {
        switch (state)
        {
            case CharacterState.Move:
                animator.SetBool("IsAttack", false);
                animator.SetBool("IsDamage", false);
                break;
            case CharacterState.Attack:
                animator.SetBool("IsAttack", true);
                animator.SetBool("IsDamage", false);
                break;
            case CharacterState.Damage:
                animator.SetBool("IsAttack", false);
                animator.SetBool("IsDamage", true);
                break;
            default:
                break;
        }
    }
}
