using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamOne
{
    [CreateAssetMenu]
    [System.Serializable]
    public class PNJData : ScriptableObject
    {
        [SerializeField] private string name;
        [SerializeField] private List<Sprite> allSprites = new List<Sprite>();
        
        private DialogueData actualDialogueData;
        public DialogueData ActualDialogueData { get => actualDialogueData; set => actualDialogueData = value; }
    }
}
