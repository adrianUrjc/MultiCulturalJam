
using UnityEngine;

public class Door : InteractionReceiver
{
    [SerializeField]
    GameObject roomObject;
    [SerializeField]
    GameObject notRoomObject;

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
        roomObject.SetActive(true);
        notRoomObject.SetActive(false);
        //move the player?
    }
    void CloseRoom()
    {
        roomObject.SetActive(false);
        notRoomObject.SetActive(true);
    }


}
