using System.Collections;
using System.Collections.Generic;
using TeamOne_SimpleJSON;
using UnityEngine;
using UnityEngine.Networking;

namespace TeamOne
{
    public class SetupDatabase : MonoBehaviour
    {
        [Header("Google Sheet")]
        JSONNode jsonFile = null;
        public JSONNode JsonFile => jsonFile;

        #region Google Sheet

        public void GetDataSheet()
        {
            StartCoroutine(ObtainSheetData());
        }

        private IEnumerator ObtainSheetData()
        {
            string link = "https://sheets.googleapis.com/v4/spreadsheets/1_jPq4mxbnY9FryTgnZsTdCUDjkSbsSF9OOT0NQcUu1A/values/Feuille%201?key=AIzaSyDtjLyq5VBk72Gs6o-X_aH_fzj0k5xG_us";
            UnityWebRequest www = UnityWebRequest.Get(link);
            yield return www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError || www.timeout > 2)
            {
                Debug.Log("Error" + www.error);
            }
            else
            {
                string json = www.downloadHandler.text;
                jsonFile = JSON.Parse(json);
            }
        }

        #endregion
    }
}