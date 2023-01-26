using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
namespace Team01
{
    public class Night : MonoBehaviour
    {
        //[SerializeField] private float duration;
        [SerializeField] private SpriteRenderer spriteNight;
        [SerializeField] private SpriteRenderer spriteLogo;

        public void StartNight()
        {
            Sequence NightAnime = DOTween.Sequence();

            NightAnime.Insert(0, spriteNight.DOFade(0, 0));
            NightAnime.Insert(0, spriteLogo.DOFade(0, 0f));
            NightAnime.Append(spriteNight.DOFade(1, 2f));
            NightAnime.Append(spriteLogo.DOFade(1, 1));
            NightAnime.AppendInterval(2f);
            NightAnime.Append(spriteLogo.DOFade(0, 1));
            NightAnime.Append(spriteNight.DOFade(0, 2));
        }

    }
}