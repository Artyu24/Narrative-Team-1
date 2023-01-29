using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace TeamOne
{
    public class MenuButtons : MonoBehaviour
    {
        //la valeur que tu dois choper pour la langue
        public int languageValue;

        [SerializeField] private GameObject ButtonEng, ButtonFR;
        [SerializeField]private GameObject menu;
        //fonction call par le bouton fr
        public void SetFR()
        {
            DialogueManager.instance.langageID = 0;
            AudioManager.instance.PlayRandom(SoundState.ButtonFR);
            ButtonEng.SetActive(false);
            ButtonFR.SetActive(true);
        }
        //fonction call par le bouton Eng
        public void SetENG()
        {
            DialogueManager.instance.langageID = 1;
            AudioManager.instance.PlayRandom(SoundState.ButtonENG);
            ButtonEng.SetActive(true);
            ButtonFR.SetActive(false);
        }

        //open et close le menu
        public void CloseMenu()
        {
            AudioManager.instance.PlayRandom(SoundState.click);
            menu.SetActive(false);
        }
        public void OpenMenu()
        {
            AudioManager.instance.PlayRandom(SoundState.click);
            menu.SetActive(true);
        }
        //bouton volume
        public void SetVolume()
        {
            AudioManager.instance.SwitchSoundParameter();
            AudioManager.instance.PlayRandom(SoundState.click);
        }
    }
}
