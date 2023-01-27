using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TeamOne;
using UnityEngine;

namespace TeamOne
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;

        [Header("Database")] 
        [SerializeField] private PNJDatabase pnjDatabase;
        [SerializeField] private DialogueDatabase dialogueDatabase;
        [SerializeField] private TextDatabase textDatabase;
        public PNJDatabase PnjDatabase => pnjDatabase;
        public DialogueDatabase DialogueDatabase => dialogueDatabase;
        public TextDatabase TextDatabase => textDatabase;


        [Header("NewsPaper")] 
        [SerializeField] private NewsPaper newsPaper;

        [Header("GameFeel")] 
        [SerializeField] private CharacterAnimation charaEffect;
        [SerializeField] private Night nightEffect;

        [Header("Actual Data")]
        public List<PNJData> pnjPhaseOneClone;
        public List<PNJData> pnjPhaseTwoClone;
        private PNJData actualPNJ;
        public PNJData ActualPNJ => actualPNJ;
        private int actualPNJID = 0;
        private int actualDay = 0;
        private bool inNightMode = false;

        private void Awake()
        {
            if(instance == null)
                instance = this;

            foreach (PNJData data in pnjDatabase.pnjPhaseOne)
            {
                pnjPhaseOneClone.Add(Instantiate(data));
            }
            foreach (PNJData data in pnjDatabase.pnjPhaseTwo)
            {
                pnjPhaseTwoClone.Add(Instantiate(data));
            }
        }

        public void InitDialogue()
        {
            if (!inNightMode)
            {
                actualPNJ = GetTodayPnjList()[actualPNJID];
                charaEffect.InitCharacter(actualPNJ.AllSprites[actualPNJ.ActualDialogueData.SpriteId]);
            }
        }

        public void NextDialogue()
        {
            DialogueManager.instance.InitDialogue(actualPNJ.ActualDialogueData);
        }

        public void SwipeChoice(bool choice)
        {
            string key = "";
            if (!choice)
                key = actualPNJ.ActualDialogueData.GoodChoiceKey;
            else
                key = actualPNJ.ActualDialogueData.BadChoiceKey;

            actualPNJ.ActualDialogueData = dialogueDatabase.GetDialogueData(key);
            charaEffect.ExitAnime();
            NextPNJ();
        }

        private void NextPNJ()
        {
            List<PNJData> actualPnjList = GetTodayPnjList();
            
            actualPNJID++;

            if (actualPNJID >= actualPnjList.Count)
            {
                inNightMode = true;
                nightEffect.StartNight();
            }

            DialogueManager.instance.DialogueText.text = "";
            charaEffect.ExitAnime();
        }

        public void SwitchDay()
        {
            List<PNJData> listTemp = GetTodayPnjList();

            //News Paper
            newsPaper.gameObject.SetActive(true);
            newsPaper.InitNews(textDatabase.GetText(listTemp[0].ActualDialogueData.DialogueLine), textDatabase.GetText(listTemp[1].ActualDialogueData.DialogueLine));
            
            //Next Dialogue for PNJ
            listTemp[0].ActualDialogueData = dialogueDatabase.GetDialogueData(listTemp[0].ActualDialogueData.NextChoiceKey);
            listTemp[1].ActualDialogueData = dialogueDatabase.GetDialogueData(listTemp[1].ActualDialogueData.NextChoiceKey);

            //Jour suivant
            actualPNJID = 0;
            inNightMode = false;
            actualDay = (actualDay + 1) % 2;
        }

        private List<PNJData> GetTodayPnjList()
        {
            List<PNJData> actualPnjList = new List<PNJData>();

            if (actualDay % 2 == 0)
                actualPnjList = pnjPhaseOneClone;
            else
                actualPnjList = pnjPhaseTwoClone;

            return actualPnjList;
        }
    }
}
