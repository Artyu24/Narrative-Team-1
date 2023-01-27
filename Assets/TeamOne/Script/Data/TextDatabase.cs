using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

namespace TeamOne
{
    [CreateAssetMenu]
    public class TextDatabase : ScriptableObject
    {
        public Row[] rows;

        public void CreateNewShape(int nbrRow, int nbrColumn)
        {
            rows = new Row[nbrRow];

            for (int i = 0; i < nbrRow; i++)
            {
                rows[i] = new Row(nbrColumn);
            }
        }

        public string GetText(int id, int langage = 0)
        {
            return rows[id].columns[DialogueManager.instance.langageID];
        }
    }

    [System.Serializable]
    public class Row
    {
        public string[] columns;

        public Row(int size)
        {
            columns = new string[size];
        }
    }
}
