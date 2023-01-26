using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamOne
{
    public class Sword : Draggable
    {
        [SerializeField][Range(0,50)] private float middleOffset = 10;

        private SpriteRenderer spriteRenderer;
        // Start is called before the first frame update
        void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        // Update is called once per frame
        void Update()
        {
            Vector3 _tPos = Camera.main.WorldToScreenPoint(transform.position);
            if (_tPos.x < Screen.width / 2 - (Screen.width * middleOffset / 100))//Left
                OnLeftSide();
            else if(_tPos.x > Screen.width / 2 + (Screen.width * middleOffset / 100))//Right
                OnRightSide();
            else//Middle
                OnMiddleSide();

            transform.DORotate(new Vector3(transform.rotation.x, transform.rotation.y, (-transform.position.x - 0) * 20), .25f);
        }

        private void OnRightSide()
        {
            spriteRenderer.DOColor(Color.red, .5f);            
        }
        private void OnLeftSide()
        {
            spriteRenderer.DOColor(Color.green, .5f);
        }
        private void OnMiddleSide()
        {
            spriteRenderer.DOColor(Color.blue, .5f);
        }        
    }
}

