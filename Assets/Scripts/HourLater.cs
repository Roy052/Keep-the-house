using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HourLater : MonoBehaviour
{
    GameManager gm;

    [SerializeField] GameObject dark;
    [SerializeField] Text text;

    private void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        text.text = "";
        StartCoroutine(HourLaterCoroutine());
    }

    IEnumerator HourLaterCoroutine()
    {
        yield return new WaitForSeconds(1);
        StartCoroutine(FadeManager.FadeIn(dark.GetComponent<SpriteRenderer>(), 2));
        yield return new WaitForSeconds(2);
        string hour1 = "1 Hour Later";
        for (int i = 0; i < hour1.Length; i++)
        {
            text.text += hour1[i];
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(hour1.Length * 0.1f);
        yield return new WaitForSeconds(1);
        text.text = "";
        StartCoroutine(FadeManager.FadeOut(dark.GetComponent<SpriteRenderer>(), 2));
        yield return new WaitForSeconds(2);
        yield return new WaitForSeconds(1);
        gm.ToMyRoom();
    }
}
