using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System.Collections.Generic;
using System.Collections;

public class BottomBarView : MonoBehaviour
{
    
    [SerializeField] private List<BottomBarButtonModifier> activeBottomBarButtons;
    [SerializeField] private int selectedBtnIdx;

    public event UnityAction<int> ContentActivated;
    public event UnityAction Closed;

    void OnEnable() 
    {
       for(int i = 0; i < activeBottomBarButtons.Count; i++) 
        {
            activeBottomBarButtons[i].onButtonSelected+= SetButtonState;
        }

        Closed?.Invoke(); // No buttons currently selected so fire event on start up?        

    }

    void OnDisable()
    {
       for(int i = 0; i < activeBottomBarButtons.Count; i++) 
        {
            activeBottomBarButtons[i].onButtonSelected -= SetButtonState;
        }

        Closed?.Invoke(); // Assuming we are removing the options so dispatching event.
    }

    public void SetButtonState(int idx)
    {    
        if(idx == null) return; // would need to introduce more extensive validation but this will suffice for now.
        
        for(int i = 0; i < activeBottomBarButtons.Count; i++) 
        {
            if(i != idx) // set any other buttons that might be selecet to unselected
            {
                // Debug.Log("unset button " + activeBottomBarButtons[i].buttonId);
                activeBottomBarButtons[i].ShowDeselectedButtonState();
            }
            
            if(i == idx) // set current selection to active state
            {
                // Debug.Log("set button " + activeBottomBarButtons[i].buttonId);
                activeBottomBarButtons[i].ShowSelectedButtonState();
            }
        }
        
        DispatchContentActivated(idx);
    }

    public void DispatchContentActivated(int idx)
    {
        ContentActivated?.Invoke(idx);
    } 
}
