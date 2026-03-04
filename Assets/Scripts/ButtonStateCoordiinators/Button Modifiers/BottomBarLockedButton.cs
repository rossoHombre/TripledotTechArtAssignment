using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using DG.Tweening;
using System.Collections.Generic;
using System.Collections;
using System;

public class BottomBarLockedButton : SimpleButtonBaseClass
{
    [Header("TARGET COMPONENTS")]
    [SerializeField] private RectTransform lockIcon;
    [SerializeField] private LayoutElement buttonLayoutElement;
    
    [Header("LAYOUT ELEMENT VARIABLES TO PUSH BUTTONS OUT")]
    [SerializeField] private float startWidth = 150f;
    [SerializeField] private float hoverWidth = 320f;      
    [SerializeField] private float expandDuration = 0.35f;    
    [SerializeField] private float contractDuration = 0.2f;    
    [SerializeField] private Ease expandEase = Ease.OutBounce;
    [SerializeField] private Ease contractEase = Ease.OutBounce;

    [Header("WIGGLY ANIMATION PROPERTIES WHEN LOCLS ARE SELECTED")]
    [SerializeField] private float shakeDuration = 0.5f;
    [SerializeField] private float shakeStrength = 20f;    
    [SerializeField] private int shakeVibrato = 3;    
    [SerializeField] private float shakeRandomness = 45f; 
    
 
    public override void OnPointerEnter(PointerEventData eventData)
    {
        KillTweens();
        
        base.OnPointerEnter(eventData);
        buttonLayoutElement.DOPreferredSize(new Vector2(hoverWidth,0),expandDuration,true).SetEase(expandEase);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        KillTweens();

        base.OnPointerExit(eventData);
        buttonLayoutElement.DOPreferredSize(new Vector2(startWidth,0),contractDuration,true).SetEase(contractEase);
    }

    public override void OnPointerUp(PointerEventData eventData)
    { 
        KillTweens();

        base.OnPointerUp(eventData);

        lockIcon.DOShakeAnchorPos(shakeDuration, shakeStrength, shakeVibrato, shakeRandomness,false, false).SetEase(Ease.InOutSine);
    }
    
    private void KillTweens()
    {
        lockIcon?.DOKill();
        buttonLayoutElement?.DOKill();
    }
    
}
