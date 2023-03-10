using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamOne
{
    public class CharacterChoice : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer sprite;
        public bool exitFinished = false;
        [SerializeField] private ChoseName choseName;

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
            EnterAnime.Join(sprite.DOFade(0f, 0));
            EnterAnime.Join(sprite.DOColor(Color.black, 0f));
            EnterAnime.Append(transform.DOMoveY(-1, 3f));
            EnterAnime.Join(transform.DORotate(new Vector3(0, 0, 0), 3f).SetEase(Ease.OutBack));
            EnterAnime.Insert(1.5f, sprite.DOFade(1f, 3f)).OnComplete(() => choseName.UI.SetActive(true));
            EnterAnime.Join(sprite.DOColor(Color.white, 3f));            
        }

        //elle tu l'appel quand le dialogue est finis
        public void ExitAnime()
        {
            choseName.UI.SetActive(false);
            Sequence ExitAnime = DOTween.Sequence();
            ExitAnime.Insert(1, transform.DORotate(new Vector3(0, 0, -144), 3f).SetEase(Ease.InBack));
            ExitAnime.Insert(1f, sprite.DOColor(Color.black, 2f));
            ExitAnime.Insert(1.5f, sprite.DOFade(0f, 3f)).OnComplete(() => choseName.SetUpCharacter());
        }
    }
}
