using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using System.Collections.Generic;
using System.Collections;

public class LevelCompleteCoordinator : MonoBehaviour
{
    [Header("SHOW PRORPERTIES")]
    [SerializeField] private float fadeInTime = 0.3f;
    [SerializeField] private float startingScale = 0.2f;
    [SerializeField] private float transformInTime = 0.3f;
    [SerializeField] private Ease customEaseForShow = Ease.Linear;

   [Header("IDLE PRORPERTIES")]
    [SerializeField] private float showingScale = 1f;
    [SerializeField] private float initialDelay = 0.4f;
    [SerializeField] private float delayDifferential = 0.25f;

    [Header("EXIT PRORPERTIES")]
    [SerializeField] private float exitScale = 0f;
    [SerializeField] private float transformOutTime = 0.2f;
    [SerializeField] private float transformYOut = -2000f;
    [SerializeField] private Ease customEaseForClose = Ease.Linear;

    [Header("CONTAINER TARGETS")]
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private RectTransform mainContent;
    [SerializeField] private RectTransform background;
    [SerializeField] private RectTransform star;
    [SerializeField] private Image starGlow;
    [SerializeField] private GameObject parentCanvas;

    [SerializeField] private List<GameObject> elements = new();
    [SerializeField] private List<CountUpScript> countups;
    
    [Header("COUNTUP TEXT")]
    [SerializeField] private TextMeshProUGUI counterText;
    [SerializeField] private float countUpDuration;
    [SerializeField] private int startValue;
    [SerializeField] private int endValue;
    [SerializeField] private Ease countUpEase = Ease.Linear;

    [Header("PARTICLES")]
    [SerializeField] private GameObject particleContainer;

    private RectTransform particleContainerRect;


    private void StartCelebrationSequence()
    {
        ResetTransforms();
        particleContainer.SetActive(true);
        parentCanvas.SetActive(true);
        background.DOScale(showingScale,transformInTime).SetEase(Ease.InOutElastic).OnComplete(()=>{
            SetContentToAnimateIn();
            canvasGroup.DOFade(1f,fadeInTime);
            mainContent.DOScale(showingScale,transformInTime).SetEase(customEaseForShow).OnComplete(AnimateContents);
            starGlow.rectTransform.DOLocalRotate(new Vector3(0f,0f,360f),25f,RotateMode.FastBeyond360).SetLoops(-1,LoopType.Restart).SetRelative().SetEase(Ease.Linear);
            

        });
    }
    
    private void BackToGame()
    {
        particleContainerRect = particleContainer.GetComponent<RectTransform>();
        particleContainerRect.DOScale(0f,transformOutTime).SetEase(Ease.OutCirc).OnComplete(()=> particleContainer.SetActive(false));
       
        mainContent.DOScale(0f,transformOutTime).OnComplete(ScaleOutContentOnClose).SetDelay(0.25f).OnComplete(()=>{
            background.DOScale(exitScale,transformInTime).SetEase(Ease.InOutElastic).OnComplete(()=>parentCanvas.SetActive(false));     
            
        });    
        ResetCounterTextValues();

    }

    private void ScaleOutContentOnClose(){
        transformYOut = -Screen.height - Screen.height;
        mainContent.DOLocalMoveY(transformYOut,transformOutTime).SetEase(customEaseForClose);     
    }

    private void ResetTransforms()
    {        
        canvasGroup.alpha = 0f;
        mainContent.transform.localPosition = new Vector3(0f, 0f,0f);
        mainContent.transform.localScale = new Vector3(0f, startingScale,0f);
        background.transform.localPosition = new Vector3(0f, 0f,0f);
        background.transform.localScale = new Vector3(0f, 0f,0f);
        particleContainerRect = particleContainer.GetComponent<RectTransform>();
        particleContainerRect.DOScale(1f,0f);
    }

    private void AnimateContents()
    {
    
        var delay = initialDelay;
    
        foreach (var elem in elements)
        {
           elem.transform.DOScale(showingScale,transformInTime).SetDelay(delay).SetEase(Ease.OutCirc);
           delay += delayDifferential;
        }
        CountUpTextValues();
    
    }

    private void SetContentToAnimateIn()
    {
        //Based on which items you want to animate in after the screen is shown, this sets those to 0 scale
            foreach (var elem in elements)
        {
            elem.transform.localScale = new Vector3(0f, 0f,0f);
        }
    }
    
    private void CountUpTextValues() 
    {
        foreach (var countup in countups)
            {
                countup.CountUpText();
            }
    }
    
    private void ResetCounterTextValues() 
    {
        foreach (var countup in countups)
            {
                var textMeshCounter = countup.GetComponent<TextMeshProUGUI>();
                textMeshCounter.text = "0";
            }
    }
}


