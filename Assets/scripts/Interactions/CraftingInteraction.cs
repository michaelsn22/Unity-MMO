using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CraftingInteraction :  MonoBehaviour, IInteractable
{
    public UnityAction<IInteractable> OnInteractionComplete { get; set; }
    public Temp temp;
    
    public void Interact(Interactor interactor, out bool interactSuccessful)
    {
        temp.OpenQuestWindow();
        interactSuccessful = true;
        
    }
    //was empty brackets and nothing inside.
    public void EndInteraction()
    {
        temp.CloseQuestWindow();
        //if player moves away
    }

}
