using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEditor;
using UnityEngine.InputSystem;
using Microsoft.Unity.VisualStudio.Editor;

//Modifier to add custom translation of rects

public class SimpleButtonTranslationModifier : SimpleButtonBaseClass
{

    [SerializeField] private float hideScale = 0f;
    [SerializeField] private float showScale = 1f;
    [SerializeField] private float endPos = 600f;
    [SerializeField] private float initalPos = 600f; // before selection is made the x co-ordinate that the 
    [SerializeField] private float duration = 0.3f;
    [SerializeField] private Ease customEaseForPointerEnter = Ease.InSine;
    [SerializeField] private Ease customEaseForPointerExit = Ease.OutSine;
    [SerializeField] private Ease customEaseForPointerUp = Ease.Linear;
    [SerializeField] private RectTransform indicator;

    [SerializeField] private RectTransform buttonContainerRect;
    [SerializeField] private BottomBarButtonModifier bottomBarButtonModifier;
    private static float selectedButtonPos;

    void Awake() 
    {
        indicator.DOScale(hideScale,0f);
    }

    void OnEnable()
    {
        bottomBarButtonModifier.onButtonSelected += UpdateSelectedButtonPosition;
    }

    void OnDisable()
    {
        bottomBarButtonModifier.onButtonSelected -= UpdateSelectedButtonPosition;
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
 
        indicator.DOAnchorPosX(endPos,duration,true).SetEase(customEaseForPointerExit);
        if(selectedButtonPos == 0f)
        {  
            indicator.DOScale(showScale,0.1f).SetEase(customEaseForPointerEnter);
        }
    }

        public override void OnPointerExit(PointerEventData eventData)
    {    
        base.OnPointerExit(eventData);    

        if(selectedButtonPos == 0f)
        {
            indicator.DOScale(hideScale,0.1f).SetEase(customEaseForPointerExit);
            indicator.DOAnchorPosX(initalPos,duration,true).SetEase(customEaseForPointerExit);
            return;
        }
        indicator.DOAnchorPosX(selectedButtonPos,duration,true).SetEase(customEaseForPointerExit);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {    
        base.OnPointerUp(eventData);
        selectedButtonPos = endPos;
        indicator.DOAnchorPosX(selectedButtonPos,duration,true).SetEase(customEaseForPointerUp);
    }

    void UpdateSelectedButtonPosition(int idx) 
    {
        if(idx == bottomBarButtonModifier.buttonId)
        {
           selectedButtonPos = endPos;
        }
    }
}
