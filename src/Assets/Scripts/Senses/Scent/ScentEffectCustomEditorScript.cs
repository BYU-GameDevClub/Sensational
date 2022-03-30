using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ScentAssign))]
public class ScentEffectCustomEditorScript : Editor
{

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        ScentAssign myScript = (ScentAssign)target;

        if (myScript.scentEffect == null && GUILayout.Button("Create Scent as Child"))
        {
            myScript.CreateChild();
        }
    }
}
