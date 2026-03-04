using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using DG.Tweening;
using TMPro;
using System.Collections.Generic;
using System.Collections;
using System;

public class BottomBarButtonModifier : SimpleButtonBaseClass
{
    [Header("TARGET COMPONENTS")]
    [SerializeField] private RectTransform icon;
    [SerializeField] private CanvasGroup textToFade;
    [SerializeField] private LayoutElement buttonLayoutElement;
    [SerializeField] public int buttonId;

    [Header("MOVE Y VARIABLES")]
    [SerializeField] private float startY = 0f;
    [SerializeField] private float targetY = 72f;
    [SerializeField] private float moveDuration = 0.3f;
    [SerializeField] private Ease moveUpEase = Ease.OutQuint;
    [SerializeField] private Ease moveDownEase = Ease.OutBounce;

    [Header("LAYOUT ELEMENT VARIABLES TO PUSH BUTTONS OUT")]
    [SerializeField] private float startWidth = 236f;
    [SerializeField] private float hoverWidth = 400f;    
    [SerializeField] private float selectedWidth = 236f;    
    [SerializeField] private float expandDuration = 0.35f;    
    [SerializeField] private float contractDuration = 0.2f;    
    [SerializeField] private Ease expandEase = Ease.OutBounce;
    [SerializeField] private Ease contractEase = Ease.OutBounce;

    [Header("SCALE VARIABLES")]
    [SerializeField] private float startScale = 1.0f;
    [SerializeField] private float endScale = 1.2f;
    [SerializeField] private float scaleInDuration = 0.3f;
    [SerializeField] private float scaleOutDuration = 0.3f;
    [SerializeField] private Ease scaleInEase = Ease.InOutSine;
    [SerializeField] private Ease scaleOutEase = Ease.OutSine;

    [Header("TEXT FADE VARIABLES")]
    [SerializeField] private float endOpacity = 1f;
    [SerializeField] private float fadeInDuration = 0.3f;
    [SerializeField] private float fadeOutDuration = 0.3f;    
    [SerializeField] private Ease fadeInEase = Ease.InOutSine;
    [SerializeField] private Ease fadeOutEase = Ease.OutSine;

    private static int currentSelectedButton = -1;
    public event Action<int> onButtonSelected;


    public override void OnPointerEnter(PointerEventData eventData)
    {
        KillTweens();
        

        // Debug.Log("OnPointerEnter " + currentSelectedButton);
        buttonLayoutElement.DOPreferredSize(new Vector2(hoverWidth,0),expandDuration,true).SetEase(expandEase);
        if(buttonId == currentSelectedButton) return;
        base.OnPointerEnter(eventData);
        ActivateSequence();
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        KillTweens();

        buttonLayoutElement.DOPreferredSize(new Vector2(startWidth,0),contractDuration,true).SetEase(contractEase);
        if(buttonId == currentSelectedButton) return;
        base.OnPointerExit(eventData);
        DeactivateSequence();
    }

    public override void OnPointerDown(PointerEventData eventData)
    { 
        KillTweens();

        base.OnPointerDown(eventData);
        ActivateSequence();
        buttonLayoutElement.DOPreferredSize(new Vector2(selectedWidth,0),contractDuration,true).SetEase(contractEase);
        DispatchSelectedButtonIndex(buttonId);
    }

    public override void OnPointerUp(PointerEventData eventData)
    { 
        base.OnPointerUp(eventData);
        // prevent issue with mobile devices not showing correct end states, could do with a refactor
        icon.DOScale(endScale,0).SetEase(scaleOutEase);
        textToFade.DOFade(endOpacity,0).SetEase(fadeOutEase);
        icon.DOAnchorPos(new Vector2(0,targetY),0).SetEase(moveDownEase);
        buttonLayoutElement.DOPreferredSize(new Vector2(selectedWidth,0),0,true).SetEase(contractEase);
    }



    public void ShowDeselectedButtonState()
    {
        DeactivateSequence();
        buttonLayoutElement.DOPreferredSize(new Vector2(startWidth,0),contractDuration,true).SetEase(contractEase);
    }

    public void ShowSelectedButtonState()
    {
        ActivateSequence();
        buttonLayoutElement.DOPreferredSize(new Vector2(selectedWidth,0),contractDuration,true).SetEase(contractEase);
    }



    private void KillTweens()
    {
        icon?.DOKill();
        textToFade?.DOKill();
        buttonLayoutElement?.DOKill();
    }

    private void ActivateSequence()
    {
        icon.DOScale(endScale,scaleInDuration).SetEase(scaleInEase);
        textToFade.DOFade(1f,fadeInDuration).SetEase(fadeInEase);
        icon.DOAnchorPos(new Vector2(0,targetY),moveDuration).SetEase(moveUpEase);
    }
    

    private void DeactivateSequence()
    {
        icon.DOScale(startScale,scaleOutDuration).SetEase(scaleOutEase);
        textToFade.DOFade(0f,fadeOutDuration).SetEase(fadeOutEase);
        icon.DOAnchorPos(new Vector2(0,startY),moveDuration).SetEase(moveDownEase);
    }


    public void DispatchSelectedButtonIndex(int idx) 
    {

        // Debug.Log("DispatchSelectedButtonIndex "+currentSelectedButton);
        currentSelectedButton = idx;
        onButtonSelected?.Invoke(idx);
    }

}


