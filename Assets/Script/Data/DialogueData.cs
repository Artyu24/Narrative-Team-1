using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamOne
{
    [System.Serializable]
    public class DialogueData
    {
        private int dialogueId;
        private int spriteId;
        public int DialogueId => dialogueId;
        public int SpriteId => spriteId;

        public DialogueData wrongChoice;
        public DialogueData rightChoice;

        public DialogueData (int dialogueId, int spriteId)
        {
            this.dialogueId = dialogueId;
            this.spriteId = spriteId;
            wrongChoice = null;
            rightChoice = null;
        }
    }
}
