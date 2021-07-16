using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimTest : MonoBehaviour
{
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Singleton<InputController>.Instance.A)
        {
            anim.SetBool("IsDamage", false);
            anim.SetBool("IsAttack", true);
        }
        if (Singleton<InputController>.Instance.B)
        {
            anim.SetBool("IsAttack", false);
            anim.SetBool("IsDamage", true);
        }
        if (Singleton<InputController>.Instance.X)
        {
            anim.SetBool("IsAttack", false);
            anim.SetBool("IsDamage", false);
        }
    }
}
