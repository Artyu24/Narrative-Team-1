using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TeamOne
{
    public class ChoseName : MonoBehaviour
    {
        [SerializeField] private PNJDatabase pnj;
        private int currentCharacter = 0;
        [SerializeField] private GameObject spriteCon;

        [Header("Changing things")]
        [SerializeField] private TextMeshProUGUI dialogue;
        [SerializeField] private TMP_InputField nameInputField;
        [SerializeField] private TextMeshProUGUI namePreview;
        [SerializeField] public GameObject UI;

        private void Start()
        {
            SetUpCharacter();
        }

        public void ConfirmName()
        {
            if(nameInputField.text != "")
            {
                pnj.pnjDatabase[currentCharacter].PnjName = nameInputField.text;
            }

            if (currentCharacter < pnj.pnjDatabase.Count - 1)
            {
                currentCharacter++; 
            }
            else
            {
                EndChoosingNames();
                return;
            }
            spriteCon.GetComponent<CharacterChoice>().ExitAnime();
        }

        public void KeepThisName()
        {
            pnj.pnjDatabase[currentCharacter].PnjName = pnj.pnjDatabase[currentCharacter].BasePnjName;
            if (currentCharacter < pnj.pnjDatabase.Count - 1)
            {
                currentCharacter++;
            }
            else
            {
                EndChoosingNames();
                return;
            }
            spriteCon.GetComponent<CharacterChoice>().ExitAnime();
        }

        public void SetUpCharacter()
        {
            namePreview.text = pnj.pnjDatabase[currentCharacter].BasePnjName;

            Debug.Log(spriteCon);
            spriteCon.GetComponent<CharacterChoice>().InitCharacter(pnj.pnjDatabase[currentCharacter].AllSprites[0]);

            if (true)//FR
            {
                dialogue.DOFade(0, 1.5f);
                dialogue.text = $"Bonjour,\r\n\r\nJe m'appel {pnj.pnjDatabase[currentCharacter].BasePnjName} mais comment toi tu veux m'appeler ?";
                dialogue.DOFade(255, 1.5f);
            }

           nameInputField.text = "";
        }

        public void EndChoosingNames()
        {
            gameObject.SetActive(false);
        }
    }
}