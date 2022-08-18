using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interactor : MonoBehaviour
{
    public Transform InteractionPoint;

    public LayerMask InteractionLayer;
    public float InteractionPointRadius = 1f;
    public bool IsInteractiing { get; private set; }

    private void Update()
    {
        //if we overlap anything that is on that layer, at that position, and within the sphere, store it in a collider array
        var colliders = Physics.OverlapSphere(InteractionPoint.position, InteractionPointRadius, InteractionLayer);
        if (colliders.Length == 0)
        {
            //Debug.Log("Not colliding with an interactable right now.");
        }
        else if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                var interactable = colliders[i].GetComponent<IInteractable>();
                if (interactable != null)
                {
                    //found a interactable item successfully
                    StartInteraction(interactable);
                }
            }
        }
    }

    void StartInteraction(IInteractable interactable)
    {
        interactable.Interact(this, out bool interactSuccessful);
        IsInteractiing = true; 
    }

    public void EndInteraction(IInteractable interactable)
    {
    }
}
