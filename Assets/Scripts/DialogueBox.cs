using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueBox : MonoBehaviour
{
    [SerializeField] DialogueManager dialogueManager;

    private void OnMouseDown()
    {
        dialogueManager.DialogueOFF();
    }
}
