using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

public class ColliderManager
{
    private List<CircleCollider> circleColliders;
    private List<RectCollider> rectColliders;

    private Subject<Collider> collisionSub = new Subject<Collider>();

    public IObservable<Collider> OnCollision
    { 
        get { return collisionSub; }
    }

    public ColliderManager()
    {
        circleColliders = new List<CircleCollider>();
        rectColliders = new List<RectCollider>();
    }

    public void RegisterCollider(CircleCollider col)
    {
        circleColliders.Add(col);
    }

    public void RegisterCollider(RectCollider col)
    {
        rectColliders.Add(col);
    }

    public void UpdateCollision()
    {
        foreach (CircleCollider c in circleColliders)
        {
            foreach (RectCollider r in rectColliders)
            {
                CircleToRect(c, r);
            }
        }
    }

    private void CircleToRect(CircleCollider c, RectCollider r)
    {
        Vector2 cPos = new Vector2(c.transform.position.x + c.Center.x, c.transform.position.y + c.Center.y);
        Vector2 rPos = new Vector2(r.transform.position.x + r.Center.x, r.transform.position.y + r.Center.y);
        Vector2 max = rPos + r.Max;
        Vector2 min = rPos + r.Min;
        float rad = c.Radius;
        float dx = min.x - cPos.x < 0 ? 0 : min.x - cPos.x;
        dx = dx < cPos.x - max.x ? cPos.x - max.x : dx;
        float dy = min.y - cPos.y < 0 ? 0 : min.y - cPos.y;
        dy = dy < cPos.y - max.y ? cPos.y - max.y : dy;
        float distSq = dx * dx + dy * dy;
        if(distSq <= rad * rad)
        {
            collisionSub.OnNext(r.GetComponent<Collider>());
            collisionSub.OnNext(c.GetComponent<Collider>());
        }
    }
}
