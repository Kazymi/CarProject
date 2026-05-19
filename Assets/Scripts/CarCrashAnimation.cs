using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CarCrashAnimation : MonoBehaviour
{
    public void OnCrash(Transform referenceObject)
    {
        transform.parent = null;
        Sequence mySequence = DOTween.Sequence();
        Vector3 addPosition = Random.Range(0, 10) > 5 ? Vector3.right : Vector3.left;
        mySequence.Append(transform.DOJump(referenceObject.transform.position - (referenceObject.transform.forward + addPosition) * 6, 3, 1, 0.4f));
        mySequence.Join(transform.DOLocalRotate(new Vector3(Random.Range(-24, 24), Random.Range(-45, 45), 180), 0.2f));
        mySequence.Join(Camera.main.transform.DOShakePosition(0.2f, 0.3f));
    }
}