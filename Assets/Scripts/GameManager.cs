using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int time;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip[] soundEffects;

    public void HourLeft()
    {
        time++;
        SceneManager.LoadScene(1);
    }

    public void SoundEffect(int closet_door_desk_light)
    {
        audioSource.clip = soundEffects[closet_door_desk_light];
        audioSource.Play();
    }
}
