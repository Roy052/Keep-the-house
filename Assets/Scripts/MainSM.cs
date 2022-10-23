using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSM : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    [SerializeField] GameObject player;
    [SerializeField] GameObject dark;
    [SerializeField] GameObject[] roomObjects;

    bool cameraFollow = false;
    Vector3 tempPosition;
    float[] timeAlphaChange = new float[3] { 0.25f, 0.3f, 0.35f };
    int time = 0;
    GameManager gm;
    private void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        time = gm.time;
        if (time == 2) LightOFF();
        else LightON();
    }

    private void Update()
    {
        if (cameraFollow)
        {
            tempPosition = player.transform.position;
            
            tempPosition.y = 0;
            tempPosition.z = -10;
            mainCamera.transform.position = tempPosition;
        }
    }
    public void CameraFollow()
    {
        cameraFollow = true;
    }

    public void CameraUnfollow()
    {
        cameraFollow = false;
    }
    
    public void LightON()
    {
        dark.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, timeAlphaChange[time]);
        Color temp = new Color(1, 1, 1, 1);
        foreach (GameObject roomObject in roomObjects)
        {
            roomObject.GetComponent<SpriteRenderer>().color = temp;
        }

    }

    public void LightOFF()
    {
        dark.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, timeAlphaChange[time] + 0.5f);
        Color temp = new Color(0, 0, 0, 1);
        foreach(GameObject roomObject in roomObjects)
        {
            roomObject.GetComponent<SpriteRenderer>().color = temp;
        }
    }
}
