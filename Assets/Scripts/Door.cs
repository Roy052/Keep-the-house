using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable
{
    bool locked = false;
    DialogueManager dialogueManager;
    GameManager gm;

    private void Start()
    {
        dialogueManager = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    public void DoorOpen()
    {
        if (!locked)
        {
            gm.SoundEffect(2);
            Interact();
        } 
        else
            dialogueManager.DialogueON("Door locked.");
    }

    public void DoorClose()
    {
        if (!locked)
        {
            gm.SoundEffect(2);
            Interact();
        }
        else
            dialogueManager.DialogueON("Door locked.");
    }

    public void DoorLock()
    {
        locked = true;
    }

    public void DoorUnlock()
    {
        locked = false;
    }
}
