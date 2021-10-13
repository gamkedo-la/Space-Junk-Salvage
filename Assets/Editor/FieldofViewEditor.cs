using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor (typeof (FieldOfView))]
public class FieldofViewEditor : Editor
{
    private void OnSceneGUI()
    {
        FieldOfView FoV = (FieldOfView)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(FoV.transform.position, Vector3.up, Vector3.forward, 360, FoV.ViewRadius);

        Vector3 ViewAngleA = FoV.DirectionFromAngle(-FoV.ViewAngle / 2, false);

        Vector3 ViewAngleB = FoV.DirectionFromAngle(FoV.ViewAngle / 2, false);

        Handles.DrawLine(FoV.transform.position, FoV.transform.position + ViewAngleA * FoV.ViewRadius);
        Handles.DrawLine(FoV.transform.position, FoV.transform.position + ViewAngleB * FoV.ViewRadius);


        Handles.DrawWireArc(FoV.transform.position, Vector3.up, Vector3.forward, 360, FoV.PeripheralRadius);

        Vector3 PeripheralAngleA = FoV.DirectionFromAngle(-FoV.PeripheralAngle / 2, false);
    
        Vector3 PeripheralAngleB = FoV.DirectionFromAngle(FoV.PeripheralAngle / 2, false);

        Handles.DrawLine(FoV.transform.position, FoV.transform.position + PeripheralAngleA * FoV.PeripheralRadius);
        Handles.DrawLine(FoV.transform.position, FoV.transform.position + PeripheralAngleB * FoV.PeripheralRadius);
    }
}
