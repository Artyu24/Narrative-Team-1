using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamOne
{
    [System.Serializable]
    public class DialogueData
    {
        [SerializeField] private int dialogueID;
        [SerializeField] private string key;
        public int DialogueId => dialogueID;
        public string Key { get => key; set => key = value; }

        [SerializeField] private int dialogueLine;
        [SerializeField] private int spriteId;
        public int DialogueLine => dialogueLine;
        public int SpriteId => spriteId;

        [SerializeField] private TextState actualTextState = TextState.DEFAULT;
        public TextState ActualTextState => actualTextState;

        [SerializeField] private string nextChoiceKey;
        [SerializeField] private string badChoiceKey;
        [SerializeField] private string goodChoiceKey;
        [SerializeField] private string addonKey;

        public string NextChoiceKey => nextChoiceKey;
        public string BadChoiceKey { get => badChoiceKey; set => badChoiceKey = value; }
        public string GoodChoiceKey { get => goodChoiceKey; set => goodChoiceKey = value; }
        public string AddonKey { get => addonKey; set => addonKey = value; }

        public DialogueData(int dialogueId, int dialogueLine, int spriteId, TextState actualTextState, string nextChoiceKey = "")
        {
            this.dialogueID = dialogueId;
            this.dialogueLine = dialogueLine;
            this.spriteId = spriteId;
            this.actualTextState = actualTextState;
            this.nextChoiceKey = nextChoiceKey;
        }
    }
}
