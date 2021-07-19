using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Character character;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        character = transform.root.GetComponent<Character>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (character.CState)
        {
            case Character.CharacterState.Move:
                animator.SetBool("IsAttack", false);
                animator.SetBool("IsDamage", false);
                break;
            case Character.CharacterState.Attack:
                animator.SetBool("IsAttack", true);
                animator.SetBool("IsDamage", false);
                break;
            case Character.CharacterState.Damage:
                animator.SetBool("IsAttack", false);
                animator.SetBool("IsDamage", true);
                break;
            default:
                break;
        }
    }
}
