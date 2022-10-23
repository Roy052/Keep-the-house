using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable
{
    bool locked = false;
    public void DoorOpen()
    {
        if(!locked) 
            Interact();
    }

    public void DoorClose()
    {
        if(!locked)
            Interact();
    }
}
