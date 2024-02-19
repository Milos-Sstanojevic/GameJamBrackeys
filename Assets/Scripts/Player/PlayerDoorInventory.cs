using System.Collections.Generic;
using UnityEngine;

public class PlayerDoorInventory : MonoBehaviour
{
    public static PlayerDoorInventory Instance { get; private set; }
    private List<DoorsController> inventoryOfPlayer;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            //Destroy(gameObject);
        }

        inventoryOfPlayer = new List<DoorsController>();
    }

    public void AddDoorToPlayerInventory(DoorsController door)
    {
        inventoryOfPlayer.Add(door);
    }
    public DoorsController TakeDoorFromInventory(DoorsController door)
    {
        return inventoryOfPlayer.Remove(door) ? door : null;
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