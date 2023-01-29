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
        JSONNode jsonAddonFile = null;
        public JSONNode JsonFile => jsonFile;
        public JSONNode JsonAddonFile => jsonAddonFile;

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

            string link2 = "https://sheets.googleapis.com/v4/spreadsheets/1_jPq4mxbnY9FryTgnZsTdCUDjkSbsSF9OOT0NQcUu1A/values/Feuille%202?key=AIzaSyDtjLyq5VBk72Gs6o-X_aH_fzj0k5xG_us";
            UnityWebRequest www2 = UnityWebRequest.Get(link2);
            yield return www2.SendWebRequest();
            if (www2.isNetworkError || www2.isHttpError || www2.timeout > 2)
            {
                Debug.Log("Error" + www2.error);
            }
            else
            {
                string json = www2.downloadHandler.text;
                jsonAddonFile = JSON.Parse(json);
            }
        }

        #endregion
    }
}