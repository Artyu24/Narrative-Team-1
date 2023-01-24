using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace TeamOne
{
    [CreateAssetMenu]
    [System.Serializable]
    public class DialogueDatabase : ScriptableObject
    {
        [SerializeField] private List<DialogueDataID> dialogueDB = new List<DialogueDataID>();
        public List<DialogueDataID> DialogueDb => dialogueDB;
    }

    [Serializable]
    public struct DialogueDataID
    {
        public string id;
        public DialogueData data;

        public DialogueDataID(string idKey, DialogueData dialogueData)
        {
            id = idKey;
            data = dialogueData;
        }
    }
}
