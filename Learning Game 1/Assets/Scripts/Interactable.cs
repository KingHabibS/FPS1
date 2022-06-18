using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    //Add or remove and InteractionEvent component to gameobject.
    public bool useEvents;


    //Shows message when looking at interactable
    public string promptMessage;

    public void BaseInteract()
    {
        if (useEvents)
            GetComponent<InteractionEvent>().OnInteract.Invoke();
        Interact();
    }

    protected virtual void Interact()
    {
        //No code. Template function to be overridden by our subclasses
    }
}
