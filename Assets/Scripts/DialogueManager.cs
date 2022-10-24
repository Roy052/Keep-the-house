using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] GameObject dialogueBox;
    [SerializeField] Text dialogueText;

    private void Start()
    {
        DialogueOFF();
    }
    public void DialogueON(string text)
    {
        dialogueText.text = "";
        dialogueBox.SetActive(true);
        dialogueText.gameObject.SetActive(true);
        StartCoroutine(PrintDialogue(text));
    }

    IEnumerator PrintDialogue(string text)
    {
        dialogueText.text = "";
        for(int i = 0; i < text.Length; i++)
        {
            dialogueText.text += text[i];
            yield return new WaitForSeconds(0.05f);
        }
    }

    public void DialogueOFF()
    {
        dialogueText.gameObject.SetActive(false);
        dialogueBox.SetActive(false);
        dialogueText.text = "";
       
    }
}
