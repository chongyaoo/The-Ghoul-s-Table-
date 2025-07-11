using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Interactable : MonoBehaviour
{
    public string promptMessage; //message displayed when hovering above interactable

    public void BaseInteract() //to call the Interact() function
    {
        Interact();
    }

    protected virtual void Interact()
    {
        //to be overwritten
    }
}
