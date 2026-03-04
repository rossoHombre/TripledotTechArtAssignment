using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

// Shows Hides Texture with a shimmer material when hovering over

public class SimpleButtonShimmerModifier : SimpleButtonBaseClass
{

    [SerializeField] private Image imageForSheen;
    [SerializeField] private float duration = 0.3f;
    [SerializeField] private Ease customEaseForPointerEnter = Ease.InSine;
    [SerializeField] private Ease customEaseForPointerExit = Ease.OutSine;
    [SerializeField] private Ease customEasePointerForDown = Ease.Linear;
    [SerializeField] private Ease customEaseForPointerUp = Ease.Linear;


    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);

        imageForSheen.DOFade(1,duration).SetEase(customEaseForPointerEnter);               

    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
        imageForSheen.DOFade(0,duration).SetEase(customEaseForPointerExit);     
    }

    public override void OnPointerDown(PointerEventData eventData)
    {    
        base.OnPointerDown(eventData);
        imageForSheen.DOFade(0,duration).SetEase(customEasePointerForDown);
    }


    public override void OnPointerUp(PointerEventData eventData)
    {    
        base.OnPointerUp(eventData);
        imageForSheen.DOFade(0,duration).SetEase(customEaseForPointerUp);
    }
}
