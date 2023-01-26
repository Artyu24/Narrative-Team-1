using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamOne
{
    public class MenuButtons : MonoBehaviour
    {
        public int laguageValue;
        [SerializeField] private GameObject ButtonEng, ButtonFR;
        [SerializeField]private GameObject menu;
        public void SetFR()
        {
            laguageValue = 0;
            AudioManager.instance.PlayRandom(SoundState.ButtonFR);
            ButtonEng.SetActive(false);
            ButtonFR.SetActive(true);
        }

        public void SetENG()
        {
            laguageValue = 1;
            AudioManager.instance.PlayRandom(SoundState.ButtonENG);
            ButtonEng.SetActive(true);
            ButtonFR.SetActive(false);
        }
        public void CloseMenu()
        {
            menu.SetActive(false);
        }
        public void OpenMenu()
        {
            menu.SetActive(true);
        }
    }
}
