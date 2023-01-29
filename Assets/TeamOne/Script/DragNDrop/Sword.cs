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

        public GameObject goodParticle;
        public GameObject badParticle;

        public void InitChoice()
        {
            switch (GameManager.instance.ActualPNJ.ActualDialogueData.ActualWeaponState)
            {
                case WeaponState.SHEARS:
                    weapon = 0;
                    break;
                case WeaponState.DAGGER:
                    weapon = 1;
                    break;
                case WeaponState.AXE:
                    weapon = 2;
                    break;
                case WeaponState.SWORD:
                    weapon = 3;
                    break;
                case WeaponState.PICKAXE:
                    weapon = 4;
                    break;
                case WeaponState.KATANA:
                    weapon = 5;
                    break;
            }

            Debug.Log(BadSr);


            BadSr.sprite = goodSprite[weapon];
            goodSr.sprite = badSprite[weapon]; 
            neutralSr.sprite = neutralSprite[weapon];
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

            goodParticle.SetActive(false);
            badParticle.SetActive(true);
        }
        private void OnLeftSide()
        {
            transform.GetChild(0).GetComponent<SpriteRenderer>().DOFade(255, .25f);
            transform.GetChild(1).GetComponent<SpriteRenderer>().DOFade(0, .25f);
            transform.GetChild(2).GetComponent<SpriteRenderer>().DOFade(0, .25f);

            goodParticle.SetActive(true);
            badParticle.SetActive(false);
        }
        private void OnMiddleSide()
        {
            transform.GetChild(0).GetComponent<SpriteRenderer>().DOFade(0, .25f);
            transform.GetChild(1).GetComponent<SpriteRenderer>().DOFade(255, .25f);
            transform.GetChild(2).GetComponent<SpriteRenderer>().DOFade(0, .25f);

            goodParticle.SetActive(false);
            badParticle.SetActive(false);
        }        
    }
}

