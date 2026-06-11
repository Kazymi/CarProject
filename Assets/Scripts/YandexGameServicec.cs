using System;
using DG.Tweening;
using TMPro;
using YG;
using UnityEngine;

public class YandexGameService : MonoBehaviour
{
    public int IntersitionCooldown;
    public int BannerTime;
    public int BannerCooldown;
    public TMP_Text timer;
    public CanvasGroup timerCanvasGroup;

    private bool isCanInterstitial;

    public event Action<string> OnRewardAdv;

    private void Awake()
    {
        if (YG2.isGameplaying == false)
        {
            YG2.GameplayStart();
        }

        InterstitialLock();
        YG2.SetBannerPosition(YG2.BannerPosition.Bottom);
        Banner();
    }

    private void OnEnable()
    {
        YG2.onRewardAdv += GiveReward;
    }

    private void OnDisable()
    {
        YG2.onRewardAdv -= GiveReward;
    }

    private void Banner()
    {
        YG2.ShowBanner();
        DOVirtual.DelayedCall(BannerTime, () =>
        {
            YG2.HideBanner();
            DOVirtual.DelayedCall(BannerCooldown, Banner);
        });
    }

    public void ShowReward(string rewardID)
    {
        InterstitialLock();
        YG2.RewardedAdvShow(rewardID);
    }

    public void TryPlayInterstitial()
    {
        if (isCanInterstitial && YG2.isTimerAdvCompleted)
        {
            timer.text = "5";
            timerCanvasGroup.DOFade(1, 0.5f).OnComplete(() =>
            {
                var sequence = DOTween.Sequence();
                for (int i = 2; i == 0; i--)
                {
                    sequence.AppendInterval(1f);
                    var i1 = i;
                    sequence.JoinCallback(() => { timer.text = i1.ToString(); });
                }

                sequence.OnComplete(() =>
                {
                    timerCanvasGroup.alpha = 0;
                    YG2.InterstitialAdvShow();
                });
            });
            InterstitialLock();
        }
    }

    private void InterstitialLock()
    {
        isCanInterstitial = false;
        DOTween.Kill("CooldownADs");
        DOVirtual.DelayedCall(IntersitionCooldown, () => isCanInterstitial = true).SetId("CooldownADs");
    }

    private void GiveReward(string rewardID)
    {
        OnRewardAdv?.Invoke(rewardID);
    }
}