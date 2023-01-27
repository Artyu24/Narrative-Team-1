using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamOne
{
    public class Sword : Draggable
    {
        [SerializeField][Range(0,50)] private float middleOffset = 10;

        private SpriteRenderer spriteRenderer;

        [SerializeField] private Sprite[] badSprite;
        [SerializeField] private SpriteRenderer BadSr;

        [SerializeField] private Sprite[] goodSprite;
        [SerializeField] private SpriteRenderer goodSr;

        [SerializeField] private Sprite[] neutralSprite;
        [SerializeField] private SpriteRenderer neutralSr;

        [SerializeField] private int weapon;        

        public void InitChoice()
        {
            Debug.Log(GameManager.instance.ActualPNJ.ActualDialogueData.ActualWeaponState);

            switch (GameManager.instance.ActualPNJ.ActualDialogueData.ActualWeaponState)
            {
                case WeaponState.SHEARS:
                    weapon = 0;
                    return;
                case WeaponState.DAGGER:
                    weapon = 1;
                    return;
                case WeaponState.AXE:
                    weapon = 2;
                    return;
                case WeaponState.SWORD:
                    weapon = 3;
                    return;
                case WeaponState.PICKAXE:
                    weapon = 4;
                    return;
                case WeaponState.KATANA:
                    weapon = 5;
                    return;
            }

            transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = badSprite[weapon];
            transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = neutralSprite[weapon];
            transform.GetChild(2).GetComponent<SpriteRenderer>().sprite = goodSprite[weapon];
        }

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
            transform.GetChild(0).GetComponent<SpriteRenderer>().DOFade(0, .25f);
            transform.GetChild(1).GetComponent<SpriteRenderer>().DOFade(0, .25f);
            transform.GetChild(2).GetComponent<SpriteRenderer>().DOFade(255, .25f);
        }
        private void OnLeftSide()
        {
            transform.GetChild(0).GetComponent<SpriteRenderer>().DOFade(255, .25f);
            transform.GetChild(1).GetComponent<SpriteRenderer>().DOFade(0, .25f);
            transform.GetChild(2).GetComponent<SpriteRenderer>().DOFade(0, .25f);
        }
        private void OnMiddleSide()
        {
            transform.GetChild(0).GetComponent<SpriteRenderer>().DOFade(0, .25f);
            transform.GetChild(1).GetComponent<SpriteRenderer>().DOFade(255, .25f);
            transform.GetChild(2).GetComponent<SpriteRenderer>().DOFade(0, .25f);
        }        
    }
}

