using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

// Scales Button using tweens where desired

public class SimpleButtonScaleModifier : SimpleButtonBaseClass
{

    [SerializeField] private float startScale = 1.0f;
    [SerializeField] private float endScale = 1.1f;
    [SerializeField] private float pressedScale = 0.98f;
    [SerializeField] private float duration = 0.3f;
    [SerializeField] private Ease customEaseForPointerEnter = Ease.InOutSine;
    [SerializeField] private Ease customEaseForPointerExit = Ease.OutSine;
    [SerializeField] private Ease customEasePointerForDown = Ease.InOutExpo;
    [SerializeField] private Ease customEaseForPointerUp = Ease.OutSine;



    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        transform.DOScale(endScale,duration).SetEase(customEaseForPointerEnter);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
        transform.DOScale(startScale,duration).SetEase(customEaseForPointerExit);
    }

    public override void OnPointerDown(PointerEventData eventData)
    {    
        base.OnPointerDown(eventData);
        transform.DOScale(pressedScale,duration).SetEase(customEasePointerForDown);
    }


    public override void OnPointerUp(PointerEventData eventData)
    {    
        base.OnPointerUp(eventData);
        transform.DOScale(startScale,duration).SetEase(customEaseForPointerUp);
    }
}
