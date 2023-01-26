using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamOne
{
    [CreateAssetMenu]
    [System.Serializable]
    public class PNJDatabase : ScriptableObject
    {
        public List<PNJData> pnjDatabase;
        public List<PNJData> pnjPhaseOne;
        public List<PNJData> pnjPhaseTwo;
    }
}
