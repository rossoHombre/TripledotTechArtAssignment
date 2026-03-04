using UnityEngine;
using UnityEngine.EventSystems;


   //Standard Button State Logic Used For All Buttons which can be extended by adding modifier scripts  

public abstract class SimpleButtonBaseClass : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler, IPointerDownHandler
{
     public virtual void OnPointerEnter(PointerEventData eventData)
    {
     
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {

    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {

    }

    public virtual void OnPointerUp(PointerEventData eventData)
    {

    }
}
