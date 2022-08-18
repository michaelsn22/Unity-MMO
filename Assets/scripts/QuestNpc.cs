using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class QuestNpc : MonoBehaviour, IInteractable
{
    public UnityAction<IInteractable> OnInteractionComplete { get; set; }
    public QuestGiver questPerson;

    public void Interact(Interactor interactor, out bool interactSuccessful)
    {
        questPerson.OpenQuestWindow();
        interactSuccessful = true;
    }
    //was empty brackets and nothing inside.
    public void EndInteraction()
    {
        //if player moves away
    }
}
