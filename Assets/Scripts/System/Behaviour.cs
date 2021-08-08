using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behaviour : MonoBehaviour
{
    protected Collider _myColider;
    // Start is called before the first frame update
    void Start()
    {
        _myColider = GetComponent<Collider>();
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateFrame();
        Debug.Log(_myColider.gameObject.name);
    }

    protected virtual void Initialize()
    {

    }

    protected virtual void UpdateFrame()
    {

    }

    protected virtual void DestroyThis()
    {
        _myColider.DeleteCollider();
        Destroy(gameObject);
    }

    public virtual void OnCollision(Collider col)
    {

    }
}
