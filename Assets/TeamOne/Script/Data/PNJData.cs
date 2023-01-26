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

        [Header("Data")]
        [SerializeField] private string pnjKeyName;
        [SerializeField] private List<Sprite> allSprites = new List<Sprite>();
        public string PnjKeyName => pnjKeyName;
        public List<Sprite> AllSprites => allSprites;

        [SerializeField] private DialogueData actualDialogueData;
        public DialogueData ActualDialogueData { get => actualDialogueData; set => actualDialogueData = value; }
    }
}
