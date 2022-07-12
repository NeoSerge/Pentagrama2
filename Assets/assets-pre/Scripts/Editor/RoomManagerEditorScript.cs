using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RoomManager))]
public class RoomManagerEditorScript : Editor
{
    
    public override void OnInspectorGUI(){
        DrawDefaultInspector();
        EditorGUILayout.HelpBox("This script create and join rooms", MessageType.Info);

        RoomManager roomManager = (RoomManager)target;

        if(GUILayout.Button("Join Random Room")){
            roomManager.JoinRandomRoom();
        }
        if(GUILayout.Button("Join Outdoor Room")){
            roomManager.OnEnterButtonClicked_Outdoor();
        }

    }
}