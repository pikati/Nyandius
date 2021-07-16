using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleCollider : MonoBehaviour
{
    //�r�w�C�r�A�N���X����ē����蔻�莝�z�͂���p�����āA�����炪����Get���Ēʒm����΂����񂶂�ˁiUniRX�ŉ����ł��邩���j
    [SerializeField]
    private Vector2 center;
    [SerializeField]
    private float radius;
    public Vector2 Center => center;
    public float Radius => radius;

    private void Start()
    {
        Singleton<GameManager>.Instance.RegisterCollider(this);
    }

    void Update()
    {

    }
}



