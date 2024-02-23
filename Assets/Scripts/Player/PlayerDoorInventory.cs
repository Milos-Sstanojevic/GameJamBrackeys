using System.Collections.Generic;
using UnityEngine;

public class PlayerDoorInventory : MonoBehaviour
{
    private List<DoorsController> inventoryOfPlayer;

    private void OnEnable()
    {
        EventManager.Instance.SubscribeToCollectedDoor(AddDoorToPlayerInventory);
        EventManager.Instance.SubscribeToPlaceDoor(TakeDoorFromInventory);
    }
    private void OnDisable()
    {
        EventManager.Instance.UnsubscribeToCollectedDoor(AddDoorToPlayerInventory);
        EventManager.Instance.UnsubscribeToPlaceDoor(TakeDoorFromInventory);
    }
    private void Awake()
    {
        inventoryOfPlayer = new List<DoorsController>();
    }

    public void AddDoorToPlayerInventory(DoorsController door)
    {
        Debug.Log("Adding");
        inventoryOfPlayer.Add(door);
    }
    public void TakeDoorFromInventory(DoorsController door)
    {
        Debug.Log("Removing");
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