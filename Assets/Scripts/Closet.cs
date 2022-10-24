using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Closet : Interactable
{
    GameManager gm;

    private void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void ClosetOpen()
    {
        gm.SoundEffect(0);
        Interact();
    }

    public void ClosetClose()
    {
        gm.SoundEffect(1);
        Interact();
    }
}
