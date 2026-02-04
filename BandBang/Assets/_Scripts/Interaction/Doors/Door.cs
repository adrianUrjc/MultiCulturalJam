
using UnityEngine;

public class Door : InteractionReceiver
{
    [SerializeField]
    GameObject[] insideObjects;

   
    [SerializeField]
    GameObject[] outsideObjects;

    bool isOpen = false;
    public override void Interact()
    {
        if (isOpen)
        {
            CloseRoom();
            isOpen = false;
        }
        else
        {
            OpenRoom();
            isOpen = true;
        }
    }
    void OpenRoom()
    {
        foreach (var obj in insideObjects)
        {
            obj.SetActive(true);
        }

        foreach (var obj in outsideObjects)
        {
            obj.SetActive(false);
        }
        //move the player?
    }
    void CloseRoom()
    {
        foreach (var obj in insideObjects)
        {
            obj.SetActive(false);
        }
        foreach (var obj in outsideObjects)
        {
            obj.SetActive(true);
        }
    }


}
