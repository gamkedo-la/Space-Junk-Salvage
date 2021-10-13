using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BackstabChecker))]
public class BackstabCheckerEditor : Editor
{
    private void OnSceneGUI()
    {
        BackstabChecker backstab = (BackstabChecker)target;
        Handles.color = Color.blue;
        Handles.DrawWireArc(backstab.transform.position, Vector3.up, Vector3.forward, 360, backstab.backstabRadius);

        Vector3 ViewAngleA = backstab.DirectionFromAngle(-backstab.BackstabAngle / 2, false);

        Vector3 ViewAngleB = backstab.DirectionFromAngle(backstab.BackstabAngle / 2, false);

        Handles.DrawLine(backstab.transform.position, backstab.transform.position + ViewAngleA * backstab.backstabRadius);
        Handles.DrawLine(backstab.transform.position, backstab.transform.position + ViewAngleB * backstab.backstabRadius);


        
    }
}
