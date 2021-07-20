using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderManager
{
    private List<CircleCollider> circleColliders;
    private List<RectCollider> rectColliders;
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
        for(int i = 0; i < rectColliders.Count; i++)
        {
            for(int j = i + 1; j < rectColliders.Count; j++)
            {
                RectToRect(rectColliders[i], rectColliders[j]);
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
            c.GetBehaviour().OnCollision(r);
            r.GetBehaviour().OnCollision(c);
        }
    }

    private void RectToRect(RectCollider r1, RectCollider r2)
    {
        Vector2 min1 = new Vector2(r1.transform.position.x, r1.transform.position.y) + r1.Center + r1.Min;
        Vector2 min2 = new Vector2(r2.transform.position.x, r2.transform.position.y) + r2.Center + r2.Min;
        Vector2 max1 = new Vector2(r1.transform.position.x, r1.transform.position.y) + r1.Center + r1.Max;
        Vector2 max2 = new Vector2(r2.transform.position.x, r2.transform.position.y) + r2.Center + r2.Max;
		if (min1.x > max2.x)
		{
			return;
		}

		if (max1.x < min2.x)
		{
			return;
		}

		if (min1.y > max2.y)
		{
			return;
		}

		if (max1.y < min2.y)
		{
			return;
		}
        r1.GetBehaviour().OnCollision(r2);
        r2.GetBehaviour().OnCollision(r1);
    }

    public void DeleteCollider(Collider col)
    {
        if (col.CType == ColliderType.Circle)
        {
            List<CircleCollider> tmp = new List<CircleCollider>();
            foreach (CircleCollider cc in circleColliders)
            {
                tmp.Add(cc);
            }
            foreach (CircleCollider cc in tmp)
            {
                if (cc.ID == col.ID)
                {
                    circleColliders.Remove(cc);
                    return;
                }
            }
        }
        else
        {
            List<RectCollider> tmp = new List<RectCollider>();
            foreach (RectCollider rc in rectColliders)
            {
                tmp.Add(rc);
            }
            foreach (RectCollider rc in tmp)
            {
                if (rc.ID == col.ID)
                {
                    rectColliders.Remove(rc);
                    return;
                }
            }
        }
    }
}
