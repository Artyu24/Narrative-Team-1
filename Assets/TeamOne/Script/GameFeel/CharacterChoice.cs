using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamOne
{
    public class CharacterChoice : MonoBehaviour
    {
        public SpriteRenderer sprite;
        public bool exitFinished = false;
        [SerializeField] private ChooseName _chooseName;

        //elle tu l'appel au momen ou le perso spawnS
        public void InitCharacter(Sprite charasprite)
        {
            sprite.sprite = charasprite;
            EnterAnime();
        }

        private void EnterAnime()
        {
            Sequence EnterAnime = DOTween.Sequence();
            EnterAnime.Insert(0, transform.DORotate(new Vector3(0, 0, 144), 0f));
            EnterAnime.Join(transform.DOMoveY(-3, 0f));
            EnterAnime.Join(transform.DOMoveX(-4.39f, 0f));
            EnterAnime.Join(sprite.DOFade(0f, 0));
            EnterAnime.Join(sprite.DOColor(Color.black, 0f));
            Tween a = transform.DOMoveY(-0.45f, 3f).OnComplete(() => {
                _chooseName.UI.DOFade(1, 1);
                _chooseName.UI.interactable = true;
            });

            EnterAnime.Append(a);
            EnterAnime.Join(transform.DOMoveX(0f, 2f));
            EnterAnime.Join(transform.DORotate(new Vector3(0, 0, 0), 2f).SetEase(Ease.OutBack));
            EnterAnime.Insert(1.5f, sprite.DOFade(1f, 2f));
            EnterAnime.Join(sprite.DOColor(Color.white, 2f));
        }

        //elle tu l'appel quand le dialogue est finis
        public void ExitAnime()
        {
            _chooseName.UI.DOFade(0, 1);
            _chooseName.UI.interactable = false;
            Sequence ExitAnime = DOTween.Sequence();
            ExitAnime.Insert(1, transform.DORotate(new Vector3(0, 0, -144), 2f).SetEase(Ease.InBack));
            ExitAnime.Insert(1f, sprite.DOColor(Color.black, 1f));
            Tween a = sprite.DOFade(0f, 2f).OnComplete(() => _chooseName.SetUpCharacter());
            ExitAnime.Insert(1.5f, a);
        }
    }
}
