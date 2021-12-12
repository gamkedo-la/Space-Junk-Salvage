using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BasicEnemyMovement))]
public class BasicEnemyMovementEditor : Editor
{
    private void OnSceneGUI()
    {
        var mov = (BasicEnemyMovement) target;
        Handles.color = Color.yellow;

        var n = mov.PatrolPoints.Length;
        for (var i = 0; i < n; i++)
        {
            var prevp = mov.PatrolPoints[(i == 0 ? n : i) - 1];
            var pp = mov.PatrolPoints[i];

            Handles.DrawDottedLine(prevp, pp, 1f);
            
            var pnew = Handles.PositionHandle(pp, Quaternion.identity);

            if (pnew != pp)
            {
                mov.PatrolPoints[i] = pnew;
                EditorUtility.SetDirty(target);
            }
        }
    }
}