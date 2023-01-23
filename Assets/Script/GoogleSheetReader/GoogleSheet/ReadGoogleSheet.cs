using TeamOne_SimpleJSON;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TeamOne;
using TeamOne_SimpleJSON;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using static System.Net.WebRequestMethods;

namespace TeamOne
{
    public class ReadGoogleSheet : MonoBehaviour
    {
        //public void GetDataSheet(bool onlyText)
        //{
        //    StartCoroutine(ObtainSheetData(onlyText));
        //}

        //private IEnumerator ObtainSheetData(bool onlyText)
        //{
        //    string link = "https://sheets.googleapis.com/v4/spreadsheets/1_jPq4mxbnY9FryTgnZsTdCUDjkSbsSF9OOT0NQcUu1A/values/Feuille%201?key=AIzaSyDtjLyq5VBk72Gs6o-X_aH_fzj0k5xG_us";
        //    UnityWebRequest www = UnityWebRequest.Get(link);
        //    yield return www.SendWebRequest();
        //    if (www.isNetworkError || www.isHttpError || www.timeout > 2)
        //    {
        //        Debug.Log("Error" + www.error);
        //    }
        //    else
        //    {
        //        string json = www.downloadHandler.text;
        //        JSONNode o = JSON.Parse(json);

        //        if(onlyText)
        //            Debug.Log("");
        //            //Call this function
        //        else
        //            Debug.Log("");
        //            //Call an other

        //        //string[] sentence = new string[dialogueId.Length];
        //        //for (int i = 0; i < dialogueId.Length; i++)
        //        //{
        //        //    sentence[i] = JSON.Parse(o["values"][dialogueId[i].lineId][dialogueId[i].columnId].ToString());
        //        //}
        //        //DialogueManager.Instance.StartDialogue(sentence);
        //    }
        //}
    }
}

//JSON.Parse(o["values"].Count) pour le nombre de ligne
//JSON.Parse(o["values"][0].Count) pour le nombre de colonne
