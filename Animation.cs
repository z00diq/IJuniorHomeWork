using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Animation : MonoBehaviour
{
    private Image _image;
    [SerializeField] Gradient _target;

    void Start()
    {
        _image = GetComponent<Image>();
        Tween tween = _image.DOGradientColor(_target, 5);
        tween.SetLoops(-1,LoopType.Yoyo);
    }
}
