using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections.Generic;

public class PopUpManager : MonoBehaviour
{
    [Header("SHOW PRORPERTIES")]
    [SerializeField] private float fadeInTime = 0.4f;
    [SerializeField] private float startingScale = 0f;
    [SerializeField] private float transformInTime = 0.7f;
    [SerializeField] private float translateYIn_From = 2000f;
    [SerializeField] private float translateYIn_To = 0f;
    [SerializeField] private Ease customEaseForShow = Ease.Linear;

    [Header("IDLE PRORPERTIES")]
    [SerializeField] private float showingScale = 1f;
    
    [Header("EXIT PRORPERTIES")]
    [SerializeField] private float fadeOutTime = 0.25f;
    [SerializeField] private float exitScale = 0.15f;
    [SerializeField] private float transformOutTime = 0.5f;
    [SerializeField] private float translateYOut_To = -1800f;
    [SerializeField] private Ease customEaseForClose = Ease.Linear;

    [Header("CONTAINER TARGETS")]
    [SerializeField]  private CanvasGroup canvasGroup;
    [SerializeField]  private RectTransform rectTransform;

    [Header("OVERLAY PROPERTIES")]
    [SerializeField] private GameObject overlay;
    [SerializeField] private float overlayOpacity;

    [Header("CANVAS")]
    [SerializeField] private GameObject parentCanvas;


    public void ShowPopUp()
    {
        ResetPopUpTransforms();
        parentCanvas.SetActive(true);

        if(overlay.gameObject != null)
        {
            overlay.GetComponent<CanvasGroup>().blocksRaycasts = true;
            overlay.GetComponent<CanvasGroup>().DOFade(overlayOpacity,0.1f);
        }
        rectTransform.DOLocalMove(new Vector3(0f,translateYIn_To,0f),transformInTime,false).SetEase(customEaseForShow);
        rectTransform.DOScale(showingScale,transformInTime).SetEase(customEaseForShow);
        canvasGroup.DOFade(1f,fadeInTime);
    }

    public void ClosePopUp()
    {
        if(overlay.gameObject != null)
        {
            overlay.GetComponent<CanvasGroup>().alpha = 1;
            overlay.GetComponent<CanvasGroup>().blocksRaycasts = false;
            overlay.GetComponent<CanvasGroup>().DOFade(0,fadeOutTime);
        }
        rectTransform.DOLocalMove(new Vector3(0f,translateYOut_To,0f),transformOutTime,false).SetEase(customEaseForClose);
        rectTransform.DOScale(exitScale,transformOutTime).SetEase(customEaseForClose);
        canvasGroup.DOFade(0f,fadeOutTime).OnComplete(()=> parentCanvas.SetActive(false));
    }

    public void ResetPopUpTransforms()
    {
        // setup initial states on initialisation 
        if(overlay.gameObject != null)
        {
            overlay.GetComponent<CanvasGroup>().alpha = 0;
            overlay.GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
        
        canvasGroup.alpha = 0f;
        rectTransform.transform.localPosition = new Vector3(0f, translateYIn_From,0f);
        rectTransform.transform.localScale = new Vector3(0f, startingScale,0f);
    }

}
