using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoseScreen : MonoBehaviour
{
    public Button Respawn;
    public Button Restart;

    public YandexGameService YandexGameService;

    private void Awake()
    {
        YandexGameService.OnRewardAdv += (string rewardID) =>
        {
            if (rewardID == "Respawn") RespawnMethod();
        };
        Respawn.onClick.AddListener(() => { YandexGameService.ShowReward("Respawn"); });
        Restart.onClick.AddListener(() =>
        {
            Time.timeScale = 1;
            YandexGameService.TryPlayInterstitial();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        });
    }

    public void Lose()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void RespawnMethod()
    {
        Debug.Log("Respawn");
        DOVirtual.DelayedCall(0.1f, () => { Time.timeScale = 1; }).SetUpdate(true);
        gameObject.SetActive(false);
    }
}