using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamOne
{
    public class Draggable : MonoBehaviour
    {
        [SerializeField][Range(0, 50)] private float validateOffset = 5;

        private Vector2 initPos;
        public Vector2 InitPos 
        { 
            get { return initPos; } 
        }

        private Quaternion initRot;
        public Quaternion InitRot
        {
            get { return initRot; }
        }

        public bool hasToReturn = true;

        [SerializeField] private bool validateRight;
        public bool ValidateRight 
        { 
            get { return validateRight; } 
        }
        [SerializeField] private bool validateLeft;
        public bool ValidateLeft
        {
            get { return validateLeft; }
        }

        private void Awake()
        {
            initPos = transform.position;
            initRot = transform.rotation;
        }
        public void ValidateZone()
        {
            Vector2 _tPos = Camera.main.WorldToScreenPoint(transform.position);

            //Right
            validateRight = transform.position.x > 1.5 ? validateRight = true : validateRight = false;
            //Left
            validateLeft = transform.position.x < -1.5 ? validateLeft = true : validateLeft = false;

        }
    }
}


