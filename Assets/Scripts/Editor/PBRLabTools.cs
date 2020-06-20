using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityAPIExtension;
namespace PBRLab
{
    public class PBRLabTools
    {
        public class TestCase
        {
            const string testCasePath = "Assets/TestCase";
            public static string ShaderParam1 = "_Roughness";
            // public static string ShaderParam2 = "_Metallic";
            public static void CreateTestCaseFromShaderGUI()
            {
                if(GUILayout.Button("Create Test Case From Shader"))
                {
                    CreateTestCaseFromShader((Shader)Selection.activeObject);
                }
            }
            public static void CreateTestCaseFromShader(Shader shaderSource)
            {
                string subTestCasePath = testCasePath + "/" + shaderSource.name;  
                //CreateDirectory
                {
                    if(!Directory.Exists(subTestCasePath))
                    {
                        Debug.Log("Sub test case path created at: " + subTestCasePath);
                        Directory.CreateDirectory(subTestCasePath);
                        AssetDatabase.ImportAsset(subTestCasePath);
                    }else
                    {
                        Debug.Log("Project exists!");
                    }
                    XEditor.FocusOnPath(subTestCasePath);
                }
                //Create Material
                {
                    string matPath = subTestCasePath + "/" + shaderSource.name.Replace("/","_") + ".mat";
                    Material mat = AssetDatabase.LoadAssetAtPath<Material>(matPath);
                    if(mat==null)
                    {
                        mat = new Material(shaderSource);
                        AssetDatabase.CreateAsset(mat, matPath);
                        AssetDatabase.ImportAsset(matPath);
                    }
                    XEditor.FocusOnPath(matPath);
                }
                //Create Instance
                {
                    for(int i = 0 ; i<10; i++)
                    {
                        var go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                        go.name = shaderSource.name.Replace("/","_") + "_" + i.ToString();
                        go.transform.position += Vector3.right * i;
                        var mr = go.GetComponent<MeshRenderer>();
                        var mat = new Material(shaderSource);
                        mat.SetFloat(ShaderParam1,(i+1)*0.1f);
                        mr.sharedMaterial = mat;

                    }
                }
            }
        }
    }

}
