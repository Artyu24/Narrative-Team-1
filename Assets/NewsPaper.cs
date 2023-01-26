using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
namespace Team01
{
    public class NewsPaper : MonoBehaviour
    {
        [SerializeField]private GameObject article1,article2;
        private void Awake()
        {

            EnterAnime();

            //test init a virer
            InitNews(null, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aliquam pulvinar urna sed sapien elementum ultricies. Phasellus bibendum nulla mi, eu lobortis erat faucibus in.",
                null, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aliquam ");
        }

        public void InitNews(Sprite SpriteArticle1, string textArticle1, Sprite SpriteArticle2,string textArticle2)
        {
            if(SpriteArticle1!=null)
                article1.GetComponentInChildren<SpriteRenderer>().sprite = SpriteArticle1;
            if (textArticle1 != null)
                article1.GetComponentInChildren<TextMeshPro>().text = textArticle1;
            if(SpriteArticle1 != null)
                article2.GetComponentInChildren<SpriteRenderer>().sprite = SpriteArticle2;
            if (textArticle2 != null)
                article2.GetComponentInChildren<TextMeshPro>().text = textArticle2;


        }

        private void EnterAnime()
        {
            Sequence enterPaperAnime = DOTween.Sequence();
            enterPaperAnime.Insert(0, transform.DOScale(new Vector3(0, 0, 0), 0f));
            enterPaperAnime.Join(transform.DORotate(new Vector3(0, 0, 600), 0));
            enterPaperAnime.Join(transform.DOMoveY(-8, 0));
            enterPaperAnime.Append(transform.DORotate(new Vector3(0, 0, -10.96f), 1f));
            enterPaperAnime.Join(transform.DOScale(new Vector3(4.15f, 4.15f, 4.15f), 1f));
            enterPaperAnime.Join(transform.DOMoveY(1, 2f).SetEase(Ease.OutExpo));
        }
        private void ExitAnime()
        {
            Sequence exitPaperAnime= DOTween.Sequence();
            exitPaperAnime.Insert(0, transform.DOScale(new Vector3(0, 0, 0), 1f).SetEase(Ease.InBack));
            
            exitPaperAnime.Append(transform.DOMoveY(-8, 0)).OnComplete(() => { gameObject.SetActive(false);});
        }
        private void OnMouseDown()
        {
            ExitAnime();
        }
    }
}