using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamOne
{
    [CreateAssetMenu]
    [System.Serializable]
    public class PNJData : ScriptableObject
    {
        [Header("Nom")]
        [SerializeField] private string pnjName;
        public string PnjName { get { return pnjName; } set { pnjName = value; } }
        [SerializeField] private string basePnjName;
        public string BasePnjName { get { return basePnjName; } }

        [Header("Data")]
        [SerializeField] private string pnjKeyName;
        [SerializeField] private List<Sprite> allSprites = new List<Sprite>();
        public string PnjKeyName => pnjKeyName;
        public List<Sprite> AllSprites => allSprites;

        [SerializeField] private DialogueData actualDialogueData;
        public DialogueData ActualDialogueData { get => actualDialogueData; set => actualDialogueData = value; }
    }
}
