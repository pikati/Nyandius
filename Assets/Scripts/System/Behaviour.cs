using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behaviour : MonoBehaviour
{
    protected Collider myColider;
    // Start is called before the first frame update
    void Start()
    {
        myColider = GetComponent<Collider>();
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateFrame();
    }

    protected virtual void Initialize()
    {

    }

    protected virtual void UpdateFrame()
    {

    }

    protected virtual void DestroyThis()
    {
        myColider.DeleteCollider();
        Destroy(gameObject);
    }

    public virtual void OnCollision(Collider col)
    {

    }
}
