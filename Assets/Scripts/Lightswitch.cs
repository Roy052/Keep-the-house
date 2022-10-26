using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightswitch : Interactable
{
    [SerializeField] MainSM mainSM;
    GameManager gm;

    private void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void LightON()
    {
        mainSM.LightON();
        gm.SoundEffect(4);
        Interact();
        objectON = true;
    }

    public void LightOFF()
    {
        mainSM.LightOFF();
        gm.SoundEffect(4);
        Interact();
        objectON = false;
        Debug.Log(objectON);
    }
}
