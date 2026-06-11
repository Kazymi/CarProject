using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class TestAnimation : MonoBehaviour
{
    public TMP_Text text;
    public Transform image;
    public AnimationCurve curve;

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(TestAnimationMethod);
    }

    public void TestAnimationMethod()

    {
        float angle = 0;
        int id = 0;

        var sequence = DOTween.Sequence();
        sequence.Append(DOTween.To(() => angle, x => angle = x, 1f, 1).OnUpdate(() =>
        {
            image.transform.localScale =
                new Vector3(curve.Evaluate(angle), curve.Evaluate(angle), curve.Evaluate(angle));
        }));
        sequence.Join(DoText(text, 1f));
        sequence.Append(text.transform.DOShakeScale(0.2f, 0.3f));
    }

    private Tweener DoText(TMP_Text textPro, float duration)
    {
        string startText = textPro.text;
        text.text = "";
        var currentId = 0;
        return DOTween.To(() => currentId, x =>
        {
            currentId = x;
            text.text = startText.Substring(0, currentId);
        }, startText.Length, duration).SetEase(Ease.OutSine);
    }
}