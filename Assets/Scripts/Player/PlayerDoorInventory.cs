using System.Collections.Generic;
using UnityEngine;

public class PlayerDoorInventory : MonoBehaviour
{
    public static PlayerDoorInventory Instance { get; private set; }
    private List<DoorsController> inventoryOfPlayer;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        inventoryOfPlayer = new List<DoorsController>();
    }

    public void AddDoorToPlayerInventory(DoorsController door)
    {
        inventoryOfPlayer.Add(door);
    }

    public DoorsController TakeDoorFromInventory(DoorsController door)
    {
        foreach (DoorsController d in inventoryOfPlayer)
        {
            if (d == door)
            {
                inventoryOfPlayer.Remove(d);
                return d;
            }
        }

        return null;
    }

    public DoorsController GetRedDoors()
    {
        foreach (DoorsController door in inventoryOfPlayer)
        {
            if (door.GetDoorColor() == DoorColor.Red && door.gameObject.active == false)
            {
                EventManager.Instance.OnTakeDoor(door.GetDoorColor());
                return door;
            }
        }

        return null;
    }

    public DoorsController GetGreenDoors()
    {
        foreach (DoorsController door in inventoryOfPlayer)
        {
            if (door.GetDoorColor() == DoorColor.Green && door.gameObject.active == false)
            {
                EventManager.Instance.OnTakeDoor(door.GetDoorColor());
                return door;
            }
        }

        return null;
    }

    public DoorsController GetBlueDoors()
    {
        foreach (DoorsController door in inventoryOfPlayer)
        {
            if (door.GetDoorColor() == DoorColor.Blue && door.gameObject.active == false)
            {
                EventManager.Instance.OnTakeDoor(door.GetDoorColor());
                return door;
            }
        }

        return null;
    }

    public List<DoorsController> GetDoorsInPlayerInventory() => inventoryOfPlayer;
}
