using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamOne
{
    [CreateAssetMenu]
    [Serializable]
    public class AddonDatabase : ScriptableObject
    {
        public List<AddonData> addonDataList = new List<AddonData>();

        public bool IsChangeHere(string possibleChangeKey)
        {
            foreach (AddonData data in addonDataList)
            {
                if (data.changingKey == possibleChangeKey)
                    return true;
            }

            return false;
        }

        public AddonData GetAddonData(string possibleChangeKey)
        {
            foreach (AddonData data in addonDataList)
            {
                if (data.changingKey == possibleChangeKey)
                    return data;
            }

            return new AddonData();
        }
    }

    [Serializable]
    public struct AddonData
    {
        public string changingKey;
        public string oldKey;
        public string newKey;

        public AddonData(string changingKey, string oldKey, string newKey)
        {
            this.changingKey = changingKey;
            this.oldKey = oldKey;
            this.newKey = newKey;
        }
    }
}
