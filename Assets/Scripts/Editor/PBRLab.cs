using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityAPIExtension;
namespace PBRLab
{
    public class PBRLabWindow : EditorWindow
    {
        //gui params
        private string shaderPath = "Assets/Shader";
        private enum Toolbar{TestCases, Tools, Directories}
        private int toolbarIndex = 0;
        private void OnGUI()
        {
            toolbarIndex = GUILayout.Toolbar(toolbarIndex, Enum.GetNames(typeof(Toolbar)));
            if(toolbarIndex == (int)Toolbar.TestCases)
            {
                var guids = AssetDatabase.FindAssets("t:shader", new string[]{shaderPath});
                foreach(var guid in guids)
                {
                    var path = AssetDatabase.GUIDToAssetPath(guid);
                    Shader shader = AssetDatabase.LoadAssetAtPath<Shader>(path);
                    if(GUILayout.Button(shader.name))
                    {
                        Selection.activeObject = shader;
                    }
                }
            }
            if(toolbarIndex == (int)Toolbar.Tools)
            {
                PBRLabTools.TestCase.CreateTestCaseFromShaderGUI();
            }
        }
        [MenuItem("Tools/PBRLab")]
        static void Start()
        {
            var window = GetWindow(typeof(PBRLabWindow));
            window.titleContent = new GUIContent("PBRLabWindow");
        }

        static void FocusOnPath()
        {

        }
    }
}
