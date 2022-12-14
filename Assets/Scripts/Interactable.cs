using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public bool objectCheck = false;
    public bool objectON = false;
    [SerializeField] Sprite onSprite, offSprite;

    public void Interact()
    {
        objectCheck = true;
        
        objectON = !objectON;

        if (objectON == false) this.GetComponent<SpriteRenderer>().sprite = offSprite;
        else this.GetComponent<SpriteRenderer>().sprite = onSprite;
    }
}
