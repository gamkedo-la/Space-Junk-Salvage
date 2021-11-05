using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RoomManager))]
public class RoomManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.DrawDefaultInspector();

        RoomManager myRoomManager = (RoomManager)target;

        if (GUILayout.Button("Update Room Size"))
        {
            myRoomManager.UpdateSize();
            EditorUtility.SetDirty(myRoomManager);
        }
    }
}
