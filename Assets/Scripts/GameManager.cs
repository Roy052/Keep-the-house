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
        if (time == 4)
            SceneManager.LoadScene(2);
        else
            SceneManager.LoadScene(1);
    }

    public void ToMyRoom()
    {
        SceneManager.LoadScene(0);
    }

    public void SoundEffect(int closetOpen_closetClose_door_desk_light_knockLeft_knockRight)
    {
        audioSource.clip = soundEffects[closetOpen_closetClose_door_desk_light_knockLeft_knockRight];
        audioSource.Play();
    }
}
