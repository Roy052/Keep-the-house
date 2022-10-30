using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndSM : MonoBehaviour
{
    GameManager gm;

    [SerializeField] GameObject monitor, me, someone, dark;
    [SerializeField] Text text;
    AudioSource audioSource;
    
    private void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        text.text = "";
        audioSource = this.GetComponent<AudioSource>();
        StartCoroutine(EndGameCoroutine());
    }

    IEnumerator EndGameCoroutine()
    {
        SpriteRenderer monitorSpriteRenderer = monitor.GetComponent<SpriteRenderer>();
        Color tempColor = monitorSpriteRenderer.color;


        StartCoroutine(FadeManager.FadeIn(me.GetComponent<SpriteRenderer>(), 3));
        while (tempColor.r >= 0.2f)
        {
            tempColor -= new Color(1,1,1,0) * Time.deltaTime / 2;
            monitorSpriteRenderer.color = tempColor;
            yield return new WaitForEndOfFrame();
        }
        
        yield return new WaitForSeconds(2);
        StartCoroutine(FadeManager.FadeIn(someone.GetComponent<SpriteRenderer>(), 2));
        yield return new WaitForSeconds(3);
        audioSource.Play();
        StartCoroutine(FadeManager.FadeIn(dark.GetComponent<SpriteRenderer>(), 0.3f));
        yield return new WaitForSeconds(1.3f);

        text.text = "Thanks for playing";
    }
}
