using System;
using System.Collections;
using System.Collections.Generic;
using TeamOne_SimpleJSON;
using UnityEditor;
using UnityEngine;
using UnityEngine.U2D;
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

            if (GUILayout.Button("GET GOOGLE SHEET"))
            {
                myObject.GetDataSheet();
            }

            if (myObject.JsonFile != null)
            {
                EditorGUILayout.HelpBox(myObject.JsonFile["values"].Count + " line detected ", MessageType.None);
                
                if (GUILayout.Button("MAJ DIALOGUE"))
                {
                    TextDatabase DB = AssetDatabase.LoadAssetAtPath<TextDatabase>("Assets/ScriptableObject/TextDatabase.asset");
                    DB.CreateNewShape(myObject.JsonFile["values"].Count, myObject.JsonFile["values"][0].Count - 6);

                    for (int i = 6; i < myObject.JsonFile["values"][0].Count; i++)
                    {
                        for (int j = 0; j < myObject.JsonFile["values"].Count; j++)
                        {
                            DB.rows[j].columns[i - 6] = JSON.Parse(myObject.JsonFile["values"][j][i].ToString());
                        }
                    }
                }

                if (GUILayout.Button("MAJ TREE"))
                {
                    DialogueDatabase D_DB = AssetDatabase.LoadAssetAtPath<DialogueDatabase>("Assets/ScriptableObject/DialogueDatabase.asset");
                    D_DB.DialogueDb.Clear();

                    Dictionary<string, List<IdSortTemp>> persoDialogueDataTemp = new Dictionary<string, List<IdSortTemp>>();

                    for (int i = 1; i < myObject.JsonFile["values"].Count; i++)
                    {
                        string key = myObject.JsonFile["values"][i][0];
                        string[] splitKey = key.Split('_');

                        DialogueData dialogueDataTemp = CreateNewDialogueData(splitKey, i, myObject.JsonFile["values"][i][5]);
                        dialogueDataTemp.Key = key;

                        if (!ContainsDialogueDataID(D_DB.DialogueDb, JSON.Parse(myObject.JsonFile["values"][i][0])))
                        {
                            D_DB.DialogueDb.Add(new DialogueDataID(JSON.Parse(myObject.JsonFile["values"][i][0]), dialogueDataTemp));

                            //On check si le perso a deja une liste complète de dialogue
                            if (!persoDialogueDataTemp.ContainsKey(splitKey[0]))
                                persoDialogueDataTemp.Add(splitKey[0], new List<IdSortTemp>());

                            //On check si dans sa liste, le perso a deja un item du même id
                            if (!ContainsIdSortTemp(persoDialogueDataTemp[splitKey[0]], splitKey[2]))
                                persoDialogueDataTemp[splitKey[0]].Add(new IdSortTemp(int.Parse(splitKey[2]), new List<DialogueData>()));

                            persoDialogueDataTemp[splitKey[0]][GetIdSortTempList(persoDialogueDataTemp[splitKey[0]], splitKey[2])].dialogueList.Add(dialogueDataTemp);
                        }
                    }

                    foreach (List<IdSortTemp> list in persoDialogueDataTemp.Values)
                    {
                        foreach (IdSortTemp idSort in list)
                        {
                            try
                            {
                                SetupDialogueData(idSort.dialogueList);
                            }
                            catch (Exception){}
                        }
                    }

                    PNJDatabase PNJ_DB = AssetDatabase.LoadAssetAtPath<PNJDatabase>("Assets/ScriptableObject/PNJDatabase.asset");
                    
                    foreach (PNJData pnj in PNJ_DB.pnjDatabase)
                    {
                        if (persoDialogueDataTemp.ContainsKey(pnj.PnjName))
                        {
                            pnj.ActualDialogueData = GetFirstDefaultDialogue(persoDialogueDataTemp[pnj.PnjName], 1);
                        }
                    }

                    EditorUtility.SetDirty(D_DB);
                    EditorUtility.SetDirty(PNJ_DB);
                    AssetDatabase.SaveAssets();
                }
            }

            EditorUtility.SetDirty(myObject);
            serializedObject.ApplyModifiedProperties();
        }

        private DialogueData CreateNewDialogueData(string[] splitKey, int id, string nextKey)
        {
            return new DialogueData(int.Parse(splitKey[2]), id, GetSpriteId(splitKey[1]), GetTextState(splitKey[3]), nextKey);
        }

        private int GetSpriteId(string id)
        {
            switch (id)
            {
                case "A":
                    return 0;
                case "B":
                    return 1;
                case "C":
                    return 2;
                case "D":
                    return 3;
                case "E":
                    return 4;
                case "F":
                    return 5;
                default: 
                    return 0;
            }
        }

        private TextState GetTextState(string id)
        {
            switch (id)
            {
                case "D":
                    return TextState.DEFAULT;
                case "G":
                    return TextState.GOOD;
                case "R":
                    return TextState.BAD;
                case "A":
                    return TextState.ADDON;
                default:
                    return TextState.DEFAULT;
            }
        }

        private void SetupDialogueData(List<DialogueData> listDialogueData)
        {
            DialogueData defaultData = null, goodData = null, badData = null, addonData = null;

            foreach (DialogueData data in listDialogueData)
            {
                switch (data.ActualTextState)
                {
                    case TextState.DEFAULT:
                        defaultData = data;
                        break;
                    case TextState.GOOD:
                        goodData = data;
                        break;
                    case TextState.BAD:
                        badData = data;
                        break;
                    case TextState.ADDON:
                        addonData = data;
                        break;
                }
            }

            if(goodData != null)
                defaultData.GoodChoiceKey = goodData.Key;
            if(badData != null)
                defaultData.BadChoiceKey = badData.Key;
            if(addonData != null)
                defaultData.AddonKey = addonData.Key;
        }

        private bool ContainsDialogueDataID(List<DialogueDataID> listDialogueDataID, string id)
        {
            foreach (DialogueDataID obj in listDialogueDataID)
            {
                if (obj.id == id)
                    return true;
            }

            return false;
        }

        private bool ContainsIdSortTemp(List<IdSortTemp> listIdSort, string id)
        {
            foreach (IdSortTemp obj in listIdSort)
            {
                if (obj.idDialogue.ToString() == id)
                    return true;
            }

            return false;
        }

        private int GetIdSortTempList(List<IdSortTemp> listIdSort, string id)
        {
            for (int i = 0; i < listIdSort.Count; i++)
            {
                if (listIdSort[i].idDialogue.ToString() == id)
                    return i;
            }

            return -1;
        }

        private DialogueData GetFirstDefaultDialogue(List<IdSortTemp> listIdSort, int id)
        {
            for (int i = 0; i < listIdSort.Count; i++)
            {
                if (listIdSort[i].idDialogue == id)
                {
                    foreach (DialogueData data in listIdSort[i].dialogueList)
                    {
                        if (data.ActualTextState == TextState.DEFAULT)
                            return data;
                    }
                }
            }

            return null;
        }
    }

    public struct IdSortTemp
    {
        public int idDialogue;
        public List<DialogueData> dialogueList;

        public IdSortTemp(int id, List<DialogueData> list)
        {
            idDialogue = id;
            dialogueList = list;
        }
    }
}

//JSON.Parse(o["values"].Count) pour le nombre de ligne
//JSON.Parse(o["values"][0].Count) pour le nombre de colonne