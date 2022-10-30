using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSM : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    [SerializeField] GameObject player;
    [SerializeField] GameObject dark;

    //Objects
    [SerializeField] GameObject[] otherObjects;
    [SerializeField] GameObject[] roomObjects;
    GameObject door;
    GameObject lightswitch;
 
    bool cameraFollow = false;
    Vector3 tempPosition;
    float[] timeAlphaChange = new float[4] { 0.25f, 0.25f, 0.25f, 0.25f };
    int time = 0;
    GameManager gm;
    DialogueManager dialogueManager;

    //Event
    public int eventCount = 0;
    [SerializeField] GameObject[] triggerPoint;
    [SerializeField] Sprite[] insideCloset;
    [SerializeField] GameObject[] creatures;
    [SerializeField] GameObject doorCreature;
    private void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        time = gm.time;

        door = GameObject.FindGameObjectWithTag("Door");
        lightswitch = GameObject.FindGameObjectWithTag("Lightswitch");
        dialogueManager = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();

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

        if (lightswitch.GetComponent<Lightswitch>().objectON == true)
            foreach (GameObject creature in creatures)
                creature.SetActive(false);

        EventOccur();
    }
    IEnumerator SetUp()
    {
        doorCreature.SetActive(false);
        yield return new WaitForSeconds(1);
        if (time == 0)
        {
            LightON();
            lightswitch.GetComponent<Lightswitch>().objectON = true;
            gm.SoundEffect(6);
            yield return new WaitForSeconds(1);
            dialogueManager.DialogueON("Sweetie, Keep the house");
            yield return new WaitForSeconds(2.5f);
        }
        else if(time == 1)
        {
            LightON();
            lightswitch.GetComponent<Lightswitch>().objectON = true;
            gm.SoundEffect(7);
            door.GetComponent<Door>().DoorLock();
            dialogueManager.DialogueON("I heard something.");
            yield return new WaitForSeconds(2.5f);
        }
        else if (time == 2)
        {
            LightON();
            lightswitch.GetComponent<Lightswitch>().objectON = true;
            gm.SoundEffect(6);
            yield return new WaitForSeconds(1);
            dialogueManager.DialogueON("Who's there?");
            yield return new WaitForSeconds(2.5f);
        }
        else if(time == 3)
        {
            Debug.Log(time);
            lightswitch.GetComponent<Lightswitch>().LightOFF();
            CreatureAppear(0);
            CreatureAppear(2);
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

        //Light
        if (lightswitch.GetComponent<Lightswitch>().objectON == false)
            return 5;

        return 0;
    }

    public void CreatureAppear(int num)
    {
        creatures[num].SetActive(true);
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
                if (door.GetComponent<Door>().objectON == false && player.transform.position.x <= triggerPoint[2].transform.position.x)
                {
                    gm.SoundEffect(5);
                    eventCount++;
                }
            }
        }
        else if(time == 2)
        {
            if(eventCount == 0)
            {
                if (player.transform.position.x <= triggerPoint[2].transform.position.x && door.GetComponent<Door>().objectCheck == true)
                {
                    otherObjects[4].GetComponent<SpriteRenderer>().sprite = insideCloset[1];
                    roomObjects[0].GetComponent<Closet>().ClosetOpen();
                    eventCount++;
                }
            }
            else if(eventCount == 1)
            {
                if (player.transform.position.x <= triggerPoint[1].transform.position.x)
                {
                    lightswitch.GetComponent<Lightswitch>().LightOFF();
                    CreatureAppear(2);
                    eventCount++;
                }
            }
            else if(eventCount == 2)
            {
                if(roomObjects[0].GetComponent<Closet>().objectON == false)
                    otherObjects[4].GetComponent<SpriteRenderer>().sprite = insideCloset[0];
                eventCount++;
            }
        }
        else if (time == 3)
        {
            if (eventCount == 0)
            {
                if (lightswitch.GetComponent<Lightswitch>().objectON == true)
                {
                    eventCount++;
                }
            }
            else if (eventCount == 1)
            {
                if (player.transform.position.x <= triggerPoint[1].transform.position.x)
                {
                    gm.SoundEffect(6);
                    doorCreature.SetActive(true);
                    door.GetComponent<Door>().objectCheck = false;
                    eventCount++;
                }
            }
            else if (eventCount == 2)
            {
                if (door.GetComponent<Door>().objectCheck == true && player.transform.position.x <= triggerPoint[3].transform.position.x)
                {
                    otherObjects[5].GetComponent<SpriteRenderer>().sprite = insideCloset[5];
                    otherObjects[6].GetComponent<SpriteRenderer>().sprite = insideCloset[3];
                    roomObjects[1].GetComponent<Closet>().ClosetOpen();
                    roomObjects[2].GetComponent<Closet>().ClosetOpen();
                    eventCount++;
                }
            }
            else if (eventCount == 3)
            {
                if (door.GetComponent<Door>().objectCheck == true && player.transform.position.x <= triggerPoint[2].transform.position.x)
                {
                    otherObjects[4].GetComponent<SpriteRenderer>().sprite = insideCloset[8];
                    roomObjects[0].GetComponent<Closet>().ClosetOpen();
                    eventCount++;
                }
            }
        }
    }
}
