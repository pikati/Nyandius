using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CircleColliderDrawer : MonoBehaviour
{
#if UNITY_EDITOR
    [DrawGizmo(GizmoType.NonSelected | GizmoType.Selected)]
    private static void DrawPointGizmos(CircleCollider col, GizmoType gizmoType)
    {
        Color color = Color.green;

        // 選択されていないときの場合は、色をタイプごと変えてみたよ。
        if ((gizmoType & GizmoType.NonSelected) != 0)
        {
            color = Color.white;
        }
        Gizmos.color = color;

        Vector2 center = new Vector2(col.transform.position.x, col.transform.position.y) + col.Center;
        Gizmos.DrawWireSphere(center, col.Radius);
    }
#endif
}
