using System.Collections;
using System.Collections.Generic;
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

        [Header("Actual Data")]
        public List<PNJData> pnjPhaseOneClone;
        public List<PNJData> pnjPhaseTwoClone;
        private PNJData actualPNJ;
        private int actualPNJID = 0;
        private int actualDay = 0;

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

        private void Start()
        {
            NextDialogue();
        }

        public void NextDialogue()
        {
            actualPNJ = GetTodayPnjList()[actualPNJID];
            DialogueManager.instance.InitDialogue(actualPNJ.ActualDialogueData);
        }

        public void SwipeChoice(bool choice)
        {
            string key = "";
            if (choice)
                key = actualPNJ.ActualDialogueData.GoodChoiceKey;
            else
                key = actualPNJ.ActualDialogueData.BadChoiceKey;

            actualPNJ.ActualDialogueData = dialogueDatabase.GetDialogueData(key);
            NextPNJ();
        }

        private void NextPNJ()
        {
            List<PNJData> actualPnjList = GetTodayPnjList();
            
            actualPNJID++;

            if (actualPNJID >= actualPnjList.Count)
            {
                actualPNJID = 0;
                actualDay = (actualDay + 1) % 2;
                SwitchDay();
                return;
            }

            NextDialogue();
        }

        private void SwitchDay()
        {
            Debug.Log("Jour suivant");

            List<PNJData> listTemp = GetTodayPnjList();

            newsPaper.gameObject.SetActive(true);
            newsPaper.InitNews(null, textDatabase.GetText(listTemp[0].ActualDialogueData.DialogueLine), null, textDatabase.GetText(listTemp[1].ActualDialogueData.DialogueLine));
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
