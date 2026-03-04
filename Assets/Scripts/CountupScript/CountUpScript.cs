using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class CountUpScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI counterText;
    [SerializeField] private float countUpDuration;
    [SerializeField] private int startValue;
    [SerializeField] private int endValue;
    [SerializeField] private Ease countUpEase = Ease.Linear;


    public void CountUpText() 
    {
       int currentValue = startValue;

        DOTween.To(() => currentValue, x => currentValue = x, endValue, countUpDuration)
            .OnUpdate(() =>
            {
                counterText.text = currentValue.ToString();
            })
            .SetEase(countUpEase).SetDelay(countUpDuration); 
    }
}
