using System;
using System.Collections;
using System.Collections.Generic;
using TeamOne_SimpleJSON;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace TeamOne
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(SetupDatabase))]
    public class SetupDatabaseDrawer : Editor
    {
        private SetupDatabase myObject => target as SetupDatabase;

        public override void OnInspectorGUI()
        {
            EditorGUILayout.BeginVertical();
            EditorGUILayout.LabelField("-------Dialogue Tool-------");
            EditorGUILayout.HelpBox("Just press the button for MAJ all the dialogue", MessageType.Info);
            EditorGUILayout.EndVertical();

            serializedObject.Update();

            if (GUILayout.Button("MAJ GOOGLE SHEET"))
            {
                myObject.GetDataSheet();
            }

            if (myObject.JsonFile != null)
            {
                EditorGUILayout.HelpBox(myObject.JsonFile["values"].Count + " line detected ", MessageType.None);
                
                if (GUILayout.Button("MAJ DIALOGUE"))
                {
                    DialogueDatabase DB = AssetDatabase.LoadAssetAtPath<DialogueDatabase>("Assets/ScriptableObject/DialogueDatabase.asset");
                    DB.CreateNewShape(myObject.JsonFile["values"].Count - 1, myObject.JsonFile["values"][0].Count - 6);

                    for (int i = 6; i < myObject.JsonFile["values"][0].Count; i++)
                    {
                        for (int j = 1; j < myObject.JsonFile["values"].Count; j++)
                        {
                            DB.rows[j - 1].columns[i - 6] = JSON.Parse(myObject.JsonFile["values"][j][i]);
                        }
                    }
                }
            }

            //if (myObject.nbrRow > 0 && myObject.nbrColumn > 0)
            //{
            //    if (myObject.nbrColumn != tempNbrColumn || myObject.nbrRow != tempNbrRow)
            //    {
            //        myObject.CreateNewShape();
            //    }

            //    for (int i = 0; i < myObject.nbrRow; i++)
            //    {
            //        EditorGUILayout.BeginHorizontal(columnStyle);

            //        for (int j = 0; j < myObject.nbrColumn; j++)
            //        {
            //            EditorGUILayout.BeginHorizontal(rowStyle);

            //            try
            //            {
            //                myObject.rows[i].columns[j] = EditorGUILayout.Toggle(myObject.rows[i].columns[j], dataFieldStyle);
            //            }
            //            catch (Exception)
            //            {
            //                myObject.CreateNewShape();
            //            }

            //            EditorGUILayout.EndHorizontal();
            //        }

            //        EditorGUILayout.EndHorizontal();
            //    }
            //}

            EditorUtility.SetDirty(myObject);
            serializedObject.ApplyModifiedProperties();
        }
    }
}

//JSON.Parse(o["values"].Count) pour le nombre de ligne
//JSON.Parse(o["values"][0].Count) pour le nombre de colonne