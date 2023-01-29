using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamOne
{
    public class CharacterAnimation : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer charaRenderer;
        Sequence idleAnime;
        //private void Start()
        //{
        //    EnterAnime();
        //}
        //elle tu l'appel au momen ou le perso spawnS
        public void InitCharacter(Sprite charasprite)
        {
            charaRenderer.sprite = charasprite;
            EnterAnime();
        }
        private void Idle()
        {
            idleAnime = DOTween.Sequence();
            idleAnime.Append(transform.DORotate(new Vector3(0, 0, -2),1f).SetEase(Ease.OutExpo));
            idleAnime.Append(transform.DORotate(new Vector3(0, 0, 2), 1f).SetEase(Ease.OutExpo));
            idleAnime.SetLoops(-1, LoopType.Yoyo);
        }
        private void EnterAnime()
        {
            Sequence EnterAnime = DOTween.Sequence();
            EnterAnime.Insert(0, transform.DORotate(new Vector3(0, 0, 144), 0f));
            EnterAnime.Join(transform.DOMoveY(-3, 0f));
            EnterAnime.Join(transform.DOMoveX(-4.39f, 0f));
            EnterAnime.Join(charaRenderer.DOFade(0f, 0));
            EnterAnime.Join(charaRenderer.DOColor(Color.black, 0f));
            Tween a = transform.DOMoveY(-0.45f, 3f).OnComplete(() => GameManager.instance.NextDialogue());

            EnterAnime.Append(a);
            EnterAnime.Join(transform.DOMoveX(0f, 2f));
            EnterAnime.Join(transform.DORotate(new Vector3(0, 0, 0), 2f).SetEase(Ease.OutBack));
            EnterAnime.Insert(1.5f, charaRenderer.DOFade(1f, 2f));
            EnterAnime.Join(charaRenderer.DOColor(Color.white, 2f)).OnComplete(()=>Idle());
        }

        //elle tu l'appel quand le dialogue est finis
        public void ExitAnime()
        {
            Sequence ExitAnime = DOTween.Sequence();
            ExitAnime.Insert(1, transform.DORotate(new Vector3(0, 0, -144), 2f).SetEase(Ease.InBack));
            ExitAnime.Insert(1f, charaRenderer.DOColor(Color.black, 1f));
            Tween a = charaRenderer.DOFade(0f, 2f).OnComplete(() => GameManager.instance.InitDialogue());
            ExitAnime.Insert(1.5f, a);
        }
    }
}
