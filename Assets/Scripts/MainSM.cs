using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSM : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    [SerializeField] GameObject player;
    [SerializeField] GameObject dark;
    [SerializeField] DialogueManager dialogueManager;

    //Objects
    [SerializeField] GameObject[] otherObjects;
    GameObject[] roomObjects;
    GameObject door;
    GameObject lightswitch;
 
    bool cameraFollow = false;
    Vector3 tempPosition;
    float[] timeAlphaChange = new float[3] { 0.25f, 0.25f, 0.25f };
    int time = 0;
    GameManager gm;

    //Event
    int eventCount = 0;
    [SerializeField] GameObject[] triggerPoint;
    private void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        time = gm.time;

        roomObjects = GameObject.FindGameObjectsWithTag("Closet");
        door = GameObject.FindGameObjectWithTag("Door");
        lightswitch = GameObject.FindGameObjectWithTag("Lightswitch");

        StartCoroutine(SetUp());
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

        EventOccur();
    }
    IEnumerator SetUp()
    {
        if(time == 0)
        {
            LightON();
            lightswitch.GetComponent<Lightswitch>().objectON = true;
            dialogueManager.DialogueON("Sweetie, Keep the house");
            yield return new WaitForSeconds(2);
        }
        else if(time == 1)
        {
            LightON();
            lightswitch.GetComponent<Lightswitch>().objectON = true;
            door.GetComponent<Door>().DoorLock();
        }
        else
        {
            Debug.Log(time);
            lightswitch.GetComponent<Lightswitch>().LightOFF();
        }
        
        player.GetComponent<Player>().moveStop = false;
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

        foreach (GameObject roomObject in otherObjects)
        {
            roomObject.GetComponent<SpriteRenderer>().color = temp;
        }

        door.GetComponent<SpriteRenderer>().color = temp;
    }

    public void LightOFF()
    {
        dark.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, timeAlphaChange[time] + 0.65f);
        Color temp = new Color(0, 0, 0, 1);
        foreach(GameObject roomObject in roomObjects)
        {
            roomObject.GetComponent<SpriteRenderer>().color = temp;
        }

        foreach (GameObject roomObject in otherObjects)
        {
            roomObject.GetComponent<SpriteRenderer>().color = temp;
        }

        door.GetComponent<SpriteRenderer>().color = temp;
    }

    public int DeskJob()
    {
        bool closed = true;
        bool objectChecked = true;
        foreach (GameObject roomObject in roomObjects)
        {
            Interactable temp = roomObject.GetComponent<Interactable>();
            if(temp.objectCheck == false)
            {
                objectChecked = false;
                break;
            }
            if(temp.objectON == true)
            {
                closed = false;
                break;
            }
        }
        //Closet
        if(closed == false)
            return 1;
        if(objectChecked == false)
            return 2;

        //Door
        Interactable temp1 = door.GetComponent<Interactable>();
        if (temp1.objectCheck == false)
            return 3;
        if (temp1.objectON == true)
            return 4;

        return 0;
    }

    public void EventOccur()
    {
        if(time == 1)
        {
            if(eventCount == 0)
            {
                if (player.transform.position.x >= triggerPoint[3].transform.position.x)
                    eventCount++;
            }
            else if(eventCount == 1)
            {
                if (player.transform.position.x <= triggerPoint[1].transform.position.x)
                {
                    door.GetComponent<Door>().DoorUnlock();
                    door.GetComponent<Door>().DoorOpen();
                    eventCount++;
                }
            }
            else if(eventCount == 2)
            {
                if (player.transform.position.x <= triggerPoint[2].transform.position.x)
                {
                    //
                    eventCount++;
                }
            }
        }
        else if(time == 2)
        {
            if(eventCount == 0)
            {
                if (player.transform.position.x <= triggerPoint[2].transform.position.x)
                {
                    //
                    eventCount++;
                }
            }
            else if(eventCount == 1)
            {
                if (player.transform.position.x <= triggerPoint[2].transform.position.x)
                {
                    //
                    eventCount++;
                }
            }
            
            
        }
    }
}
