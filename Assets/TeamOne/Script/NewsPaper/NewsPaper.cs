using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

namespace TeamOne
{
    public class NewsPaper : MonoBehaviour
    {
        [SerializeField]private GameObject article1,article2;

        public void InitNews(string textArticle1, string textArticle2)
        {
            if (textArticle1 != null)
                article1.GetComponentInChildren<TextMeshPro>().text = textArticle1;
            if (textArticle2 != null)
                article2.GetComponentInChildren<TextMeshPro>().text = textArticle2;

            EnterAnime();
        }

        private void EnterAnime()
        {
            Sequence enterPaperAnime = DOTween.Sequence();
            enterPaperAnime.Insert(0, transform.DOScale(new Vector3(0, 0, 0), 0f));
            enterPaperAnime.Join(transform.DORotate(new Vector3(0, 0, 600), 0));
            enterPaperAnime.Join(transform.DOMoveY(-8, 0));
            enterPaperAnime.Append(transform.DORotate(new Vector3(0, 0, 0), 1f));
            enterPaperAnime.Join(transform.DOScale(new Vector3(4.15f, 4.15f, 4.15f), 1f));
            enterPaperAnime.Join(transform.DOMoveY(1, 2f).SetEase(Ease.OutExpo));
        }

        private void ExitAnime()
        {
            DialogueManager.instance.DialogueBox.DOFade(1, 1f);
            Sequence exitPaperAnime= DOTween.Sequence();
            exitPaperAnime.Insert(0, transform.DOScale(new Vector3(0, 0, 0), 1f).SetEase(Ease.InBack));
            
            exitPaperAnime.Append(transform.DOMoveY(-8, 0)).OnComplete(() =>
            {
                gameObject.SetActive(false);
                GameManager.instance.InitDialogue();
            });
        }

        private void OnMouseDown()
        {
            ExitAnime();
        }
    }
}