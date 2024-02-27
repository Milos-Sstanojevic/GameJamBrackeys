using System.Collections.Generic;
using UnityEngine;

public class PlayerDoorInventory : MonoBehaviour
{
    private List<DoorsController> inventoryOfPlayer;

    private void OnEnable()
    {
        EventManager.Instance.SubscribeToPickedUpDoor(AddDoorToPlayerInventory);
        EventManager.Instance.SubscribeToPlaceDoor(TakeDoorFromInventory);
    }
    private void OnDisable()
    {
        EventManager.Instance.UnsubscribeToPickedUpDoor(AddDoorToPlayerInventory);
        EventManager.Instance.UnsubscribeToPlaceDoor(TakeDoorFromInventory);
    }
    private void Awake()
    {
        inventoryOfPlayer = new List<DoorsController>();
    }

    public void AddDoorToPlayerInventory(DoorsController door)
    {
        inventoryOfPlayer.Add(door);
    }
    public void TakeDoorFromInventory(DoorsController door)
    {
        inventoryOfPlayer.Remove(door);
    }
    public List<DoorsController> GetDoorsInPlayerInventory() => inventoryOfPlayer;
    public DoorsController this[int index]
    {
        get
        {
            return inventoryOfPlayer.Count > index ? inventoryOfPlayer[index] : null;
        }
    }
}