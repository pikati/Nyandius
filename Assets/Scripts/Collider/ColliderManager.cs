using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderManager
{
    private List<CircleCollider> circleColliders;
    private List<RectCollider> rectColliders;

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

    }

    private void CircleToRect(CircleCollider c, RectCollider r)
    {
        Vector2 cPos = new Vector2(c.transform.position.x + c.Center.x, c.transform.position.y + c.Center.y);
        Vector2 rPos = new Vector2(r.transform.position.x + r.Center.x, r.transform.position.y + r.Center.y);
        //ê‚ëŒìñÇΩÇÁÇ»Ç¢Ç∆Ç´ÇÕÇ∑ÇÆreturn
        if (cPos.x + c.Radius < rPos.x - r.Min.x) return;
        if (cPos.x - c.Radius > rPos.x + r.Max.x) return;
        if (cPos.y + c.Radius < rPos.y - r.Min.y) return;
        if (cPos.y - c.Radius > rPos.y - r.Max.y) return;
    }
}
