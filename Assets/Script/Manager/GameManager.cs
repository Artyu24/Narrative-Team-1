using System.Collections;
using System.Collections.Generic;
using TeamOne;
using UnityEngine;

namespace MyNamespace
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;

        [Header("Database")] 
        [SerializeField] private PNJDatabase pnjDatabase;

        private void Awake()
        {
            if(instance == null)
                instance = this;
        }
    }
}
