using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamOne
{
    [CreateAssetMenu]
    [System.Serializable]
    public class PNJData : ScriptableObject
    {
        [SerializeField] private string pnjName;
        [SerializeField] private List<Sprite> allSprites = new List<Sprite>();
        public string PnjName => pnjName;
        public List<Sprite> AllSprites => allSprites;

        [SerializeField] private DialogueData actualDialogueData;
        public DialogueData ActualDialogueData { get => actualDialogueData; set => actualDialogueData = value; }
    }
}
