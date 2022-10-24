using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Desk : Interactable
{
    DialogueManager dialogueManager;
    GameManager gm;

    [SerializeField] MainSM mainSM;

    private void Start()
    {
        dialogueManager = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    public void CheckEnd()
    {
        int temp = mainSM.DeskJob();
        gm.SoundEffect(3);
        switch (temp)
        {
            case 0:
                gm.HourLeft();
                break;
            case 1:
                dialogueManager.DialogueON("There is a closet that I haven't checked.");
                break;
            case 2:
                dialogueManager.DialogueON("There is an unclosed closet.");
                break;
            case 3:
                dialogueManager.DialogueON("There is a door that I haven't checked.");
                break;
            case 4:
                dialogueManager.DialogueON("There is an unclosed door.");
                break;
        }
    }
}
