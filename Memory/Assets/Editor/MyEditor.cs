using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MyEditor : EditorWindow {
    [MenuItem ("ANDRIJA/NOVI PROZOR")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(MyEditor));
    }
    private void OnGUI()
    {
        EditorGUILayout.TextField("AKI");
    }


}
