using System.Collections.Generic;
using UnityEngine;

public class DoorsController : MonoBehaviour
{
    [SerializeField] private DoorScriptableObject doorScriptableObject;

    private void Awake()
    {
        EventManager.Instance.SubscribeToPlaceDoorHorizontallyAction(PlaceDoorHorizontally);
        EventManager.Instance.SubscribeToPlaceDoorVerticallyAction(PlaceDoorVertically);
        EventManager.Instance.SubscribeToCollectDoorAction(CollectDoor);
        EventManager.Instance.SubscribeToTeleportPlayer(TeleportPlayer);
    }

    private void PlaceDoorHorizontally(DoorsController door, Vector3 position)
    {
        if (door == this)
        {
            door.transform.position = position;
        }
    }

    private void PlaceDoorVertically(DoorsController door, Vector3 position)
    {
        if (door == this)
        {
            door.transform.position = position;
            door.transform.eulerAngles = new Vector3(0, 0, 90);
        }
    }

    private void CollectDoor(DoorsController door)
    {
        if (door == this)
        {
            door.gameObject.SetActive(false);
        }
    }

    private void TeleportPlayer(DoorsController door, PlayerController player)
    {
        if (door == this)
        {
            List<DoorsController> doors = DoorsSpawnManager.Instance.GetActiveDoorsOfColor(door.GetDoorColor());
            if (doors != null)
            {
                foreach (DoorsController d in doors)
                {
                    if (d != door)
                    {
                        player.transform.position = new Vector3(d.transform.position.x, d.transform.position.y + 0.5f, d.transform.position.z);
                    }
                }
            }
        }
    }

    public DoorColor GetDoorColor() => doorScriptableObject.doorColor;


}