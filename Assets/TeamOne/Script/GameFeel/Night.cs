using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace TeamOne
{
     
    public class Night : MonoBehaviour
    {
        //[SerializeField] private float duration;
        [SerializeField] private SpriteRenderer spriteNight;
        [SerializeField] private SpriteRenderer spriteLogo;

        public void StartNight()
        {
            Sequence NightAnime = DOTween.Sequence();
            AudioManager.instance.PlayRandom(SoundState.Night);

            NightAnime.Insert(0, spriteNight.DOFade(0, 0));
            NightAnime.Insert(0, spriteLogo.DOFade(0, 0f));
            NightAnime.Append(spriteNight.DOFade(1, 2f));
            Tween b = spriteLogo.DOFade(1, 1).OnComplete(() => AudioManager.instance.PlayRandom(SoundState.Night));
            NightAnime.Append(b);
            NightAnime.AppendInterval(2f);
            Tween a = spriteLogo.DOFade(0, 1).OnComplete(() => GameManager.instance.SwitchDay());
            NightAnime.Append(a);
            NightAnime.Append(spriteNight.DOFade(0, 2));
        }
    }
}