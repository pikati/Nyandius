using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    protected float speed = 1.0f;
    protected bool isPlayer = true;
    protected bool canPierce = false;
    protected Collider myCollider;

    // Start is called before the first frame update
    void Start()
    {
        myCollider = GetComponent<Collider>();
        Initialzie();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateFrame();
    }

    protected virtual void Initialzie()
    {

    }

    protected virtual void UpdateFrame()
    {

    }
}
