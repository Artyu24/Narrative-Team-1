using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

namespace TeamOne
{
    [CreateAssetMenu]
    public class DialogueDatabase : ScriptableObject
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
