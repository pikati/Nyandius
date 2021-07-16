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
    }

    private void CircleToRect(CircleCollider c, RectCollider r)
    {
        Vector2 cPos = new Vector2(c.transform.position.x + c.Center.x, c.transform.position.y + c.Center.y);
        Vector2 rPos = new Vector2(r.transform.position.x + r.Center.x, r.transform.position.y + r.Center.y);
        Vector2 max = rPos + r.Max;
        Vector2 min = rPos - r.Min;
        float rad = c.Radius;
        if ((cPos.x > min.x) && (cPos.x < max.x) && (cPos.y > max.y + rad) && (cPos.y < min.y - rad))
        {
            Debug.Log("ata");
            return;
        }
        if ((cPos.x > min.x -rad) && (cPos.x < max.x + rad) && (cPos.y > max.y) && (cPos.y < min.y))
        {
            Debug.Log("ata");
            return;
        }
        if (Mathf.Pow(min.x - cPos.x, 2) + Mathf.Pow(max.y - cPos.y, 2) < Mathf.Pow(rad, 2))
        {
            Debug.Log("ata");
            return;
        }
        if (Mathf.Pow(max.x - cPos.x, 2) + Mathf.Pow(max.y - cPos.y, 2) < Mathf.Pow(rad, 2))
        {
            Debug.Log("ata");
            return;
        }
        if (Mathf.Pow(max.x - cPos.x, 2) + Mathf.Pow(min.y - cPos.y, 2) < Mathf.Pow(rad, 2))
        {
            Debug.Log("ata");
            return;
        }
        if (Mathf.Pow(min.x - cPos.x, 2) + Mathf.Pow(min.y - cPos.y, 2) < Mathf.Pow(rad, 2))
        {
            Debug.Log("ata");
            return;
        }
    }
}
