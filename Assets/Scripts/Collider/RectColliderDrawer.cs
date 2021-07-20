using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class RectColliderDrawer : MonoBehaviour
{
#if UNITY_EDITOR
    [DrawGizmo(GizmoType.NonSelected | GizmoType.Selected)]
    private static void DrawPointGizmos(RectCollider col, GizmoType gizmoType)
    {
        Color color = Color.green;

        // 選択されていないときの場合は、色をタイプごと変えてみたよ。
        if ((gizmoType & GizmoType.NonSelected) != 0)
        {
            color = Color.white;
        }
        Gizmos.color = color;

        Vector2 center = new Vector2(col.transform.position.x, col.transform.position.y) + col.Center;

        Gizmos.DrawLine(new Vector3(col.Min.x + center.x, col.Min.y + center.y, 0), new Vector3(col.Max.x + center.x, col.Min.y + center.y, 0));
        Gizmos.DrawLine(new Vector3(col.Min.x + center.x, col.Max.y + center.y, 0), new Vector3(col.Max.x + center.x, col.Max.y + center.y, 0));
        Gizmos.DrawLine(new Vector3(col.Min.x + center.x, col.Min.y + center.y, 0), new Vector3(col.Min.x + center.x, col.Max.y + center.y, 0));
        Gizmos.DrawLine(new Vector3(col.Max.x + center.x, col.Min.y + center.y, 0), new Vector3(col.Max.x + center.x, col.Max.y + center.y, 0));

    }
#endif
}
