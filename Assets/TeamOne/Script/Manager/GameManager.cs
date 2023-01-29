using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TeamOne;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace TeamOne
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;

        [Header("Database")] 
        [SerializeField] private PNJDatabase pnjDatabase;
        [SerializeField] private DialogueDatabase dialogueDatabase;
        [SerializeField] private TextDatabase textDatabase;
        [SerializeField] private AddonDatabase addonDatabase;
        public PNJDatabase PnjDatabase => pnjDatabase;
        public TextDatabase TextDatabase => textDatabase;

        [Header("NewsPaper")] 
        [SerializeField] private NewsPaper newsPaper;

        [Header("GameFeel")] 
        [SerializeField] private CharacterAnimation charaEffect;
        [SerializeField] private Night nightEffect;
        [SerializeField] private CanvasGroup endGame;

        [Header("Actual Data")]
        public List<PNJData> pnjPhaseOneClone;
        public List<PNJData> pnjPhaseTwoClone;
        private DialogueDatabase dialogueDBCopy;
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

            dialogueDBCopy = Instantiate(dialogueDatabase);
        }

        public void InitDialogue()
        {
            if (!inNightMode)
            {
                actualPNJ = GetTodayPnjList()[actualPNJID];

                if (IsGameFinish())
                {
                    endGame.interactable = true;

                    Sequence seq = DOTween.Sequence();
                    seq.Append(endGame.DOFade(1, 2f));
                    //seq.AppendInterval(3f).OnComplete(() => SceneManager.LoadScene("TA SCENE ICI FRANCKO"));
                    return;
                }
                else if (IsDayFinish(GetTodayPnjList()))
                {
                    actualDay = (actualDay + 1) % 2;
                    actualPNJ = GetTodayPnjList()[actualPNJID];
                }

                if (actualPNJ.IsFinish)
                {
                    actualPNJID++;
                    actualPNJ = GetTodayPnjList()[actualPNJID];
                }

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

            if (addonDatabase.IsChangeHere(key))
            {
                AddonData data = addonDatabase.GetAddonData(key);
                dialogueDBCopy.ChangeDialogueTree(data);
            }

            actualPNJ.ActualDialogueData = dialogueDBCopy.GetDialogueData(key);
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
            else if (actualPnjList[actualPNJID].IsFinish)
            {
                NextPNJ();
                return;
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
            for (int i = 0; i < listTemp.Count; i++)
            {
                DialogueData data = dialogueDBCopy.GetDialogueData(listTemp[i].ActualDialogueData.NextChoiceKey);
                if(data != null)
                    listTemp[i].ActualDialogueData = data;
                else
                {
                    listTemp[i].IsFinish = true;
                }
            }

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

        private bool IsDayFinish(List<PNJData> pnjTodayList)
        {
            foreach (PNJData pnj in pnjTodayList)
            {
                if (!pnj.IsFinish)
                    return false;
            }

            return true;
        }

        private bool IsGameFinish()
        {
            bool isEnding = true;

            isEnding = IsDayFinish(pnjPhaseOneClone);
            isEnding = IsDayFinish(pnjPhaseTwoClone);

            return isEnding;
        }
    }
}
