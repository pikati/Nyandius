using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleCollider : MonoBehaviour
{
    //ビヘイビアクラス作って当たり判定持つ奴はこれ継承して、こいつらがそれGetして通知送ればいいんじゃね（UniRXで解決できるかも）
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



