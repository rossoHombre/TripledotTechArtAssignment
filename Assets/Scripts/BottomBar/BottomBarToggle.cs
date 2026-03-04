using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BottomBarToggle : MonoBehaviour
{
    [Header("BUTTONS")]
    [SerializeField] GameObject showButton;
    [SerializeField] GameObject hideButton;
    [Header("DURATIONS")]    
    [SerializeField] float backgroundTranstionInTime = 0.35f;
    [SerializeField] float backgroundtranstionOutTime = 0.25f;
    [SerializeField] float buttonsTranstionInTime = 0.25f;
    [SerializeField] float buttonstranstionOutTime = 0.15f;
    [Header("DELAYS")]      
    [SerializeField] float delayForPop = 0.25f;
    [SerializeField] float delayForDrop = 0.15f;
    [Header("POSITIONS")]    
    [SerializeField] float showYPos = 0;
    [SerializeField] float hideYPos = -300f;
    [Header("FADES")]    
    [SerializeField] float showOpacity = 1;
    [SerializeField] float hideOpacity = 0;
    [Header("EASES")]    
    [SerializeField] Ease buttonsInEase = Ease.OutBounce;
    [SerializeField] Ease buttonsOutEase = Ease.InOutBounce;
    [SerializeField] Ease backgroundInEase = Ease.InOutCirc;
    [SerializeField] Ease backgroundOutEase = Ease.InOutCirc;
    [Header("TARGETS")]    
    [SerializeField]  RectTransform BottomBarButtons;
    [SerializeField]  RectTransform BottomBarBackingOverflow;
    [SerializeField]  RectTransform BottomBarBackingSafeArea;
    [SerializeField]  CanvasGroup BottomBarGradient;


    public void toggleBottomBarOn() 
    {
        showButton.SetActive(false);
        hideButton.SetActive(true);
        BottomBarButtons.DOAnchorPosY(showYPos,buttonsTranstionInTime).SetEase(buttonsInEase).SetDelay(delayForPop);
        BottomBarBackingOverflow.DOAnchorPosY(showYPos,backgroundTranstionInTime).SetEase(backgroundInEase);
        BottomBarBackingSafeArea.DOAnchorPosY(showYPos,backgroundTranstionInTime).SetEase(backgroundInEase);
        BottomBarGradient.DOFade(showOpacity,backgroundTranstionInTime).SetEase(backgroundInEase);
    }

    public void  toggleBottomBarOff() 
    {
        hideButton.SetActive(false);
        showButton.SetActive(true);
        BottomBarButtons.DOAnchorPosY(hideYPos,buttonstranstionOutTime).SetEase(buttonsOutEase).SetDelay(delayForDrop);
        BottomBarBackingOverflow.DOAnchorPosY(hideYPos,backgroundtranstionOutTime).SetEase(backgroundOutEase);
        BottomBarBackingSafeArea.DOAnchorPosY(hideYPos,backgroundtranstionOutTime).SetEase(backgroundOutEase);
        BottomBarGradient.DOFade(hideOpacity,backgroundtranstionOutTime).SetEase(backgroundOutEase);
    }
   
}
