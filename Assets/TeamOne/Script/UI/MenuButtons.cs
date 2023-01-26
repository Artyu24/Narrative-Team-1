using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamOne
{
    public class MenuButtons : MonoBehaviour
    {
        public int laguageValue;
        [SerializeField]private GameObject menu;
        public void SetLanguage(int id)
        {
            laguageValue = id;
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
