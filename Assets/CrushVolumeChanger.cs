using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class CrushVolumeChanger : MonoBehaviour
{
    public Volume volume;
    private Vignette vignette;

    public static CrushVolumeChanger Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void Crush()
    {
        if (volume.profile.TryGet(out vignette))
        {
            
            
            
            DOTween.To(() => vignette.intensity.value, x => vignette.intensity.value = x, 0.8f, 0.4f)
                .SetLoops(2, LoopType.Yoyo).SetEase(Ease.Linear).OnComplete(() => { vignette.intensity.value = 0; });
        }
    }
}