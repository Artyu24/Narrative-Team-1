using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamOne
{
    public class MenuButtons : MonoBehaviour
    {
        public int laguageValue;

        public void SetLanguage(int id)
        {
            laguageValue = id;
        }
    }
}
