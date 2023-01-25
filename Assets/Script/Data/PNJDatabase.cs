using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamOne
{
    [CreateAssetMenu]
    [System.Serializable]
    public class PNJDatabase : ScriptableObject
    {
        public List<PNJData> pnjDatabaseOne;
        public List<PNJData> pnjDatabaseTwo;
    }
}
