using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ChestInventory : InventoryHolder, IInteractable
{
    public UnityAction<IInteractable> OnInteractionComplete { get; set; }


    public void Interact(Interactor interactor, out bool interactSuccessful)
    {
        OnDynamicInventoryDisplayRequested?.Invoke(inventorySystem); //from inventory holder (event from there)
        interactSuccessful = true;
    }
    //was empty brackets and nothing inside.
    public void EndInteraction()
    {
        //if player moves away
    }

}
