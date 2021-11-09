using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Animation : MonoBehaviour
{
    [SerializeField] Gradient _target;
    private Image _image;

    private void Start()
    {
        _image = GetComponent<Image>();
        Tween tween = _image.DOGradientColor(_target, 5);
        tween.SetLoops(-1,LoopType.Yoyo);
    }
}
