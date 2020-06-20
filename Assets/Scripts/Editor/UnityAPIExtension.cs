using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
namespace UnityAPIExtension
{
#if UNITY_EDITOR
    public class XEditor
    {
        public static void FocusOnPath(string path)
        {
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = AssetDatabase.LoadAssetAtPath(path, typeof(UnityEngine.Object));
            EditorGUIUtility.PingObject(Selection.activeObject);
        }
    }
#endif
}
