using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int time = 0;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip[] soundEffects;

    //Unique GameManager
    private static GameManager gameManagerInstance;

    void Awake()
    {
        DontDestroyOnLoad(this);
        if (gameManagerInstance == null)
        {
            gameManagerInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void HourLeft()
    {
        time++;
        if (time == 3)
            SceneManager.LoadScene(1);
        else
            SceneManager.LoadScene(0);
    }

    public void SoundEffect(int closet_door_desk_light)
    {
        audioSource.clip = soundEffects[closet_door_desk_light];
        audioSource.Play();
    }
}
