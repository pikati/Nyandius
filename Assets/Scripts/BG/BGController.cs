using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class BGController : MonoBehaviour
{
    [SerializeField]
    private float _speed;
    // Start is called before the first frame update
    void Start()
    {
        this.UpdateAsObservable()
            .Subscribe(_ => transform.position += Vector3.left * _speed * Time.deltaTime);
    }
}
