using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDoorHandler : MonoBehaviour
{
    [SerializeField] float maxReach;
    private enum Rotation
    {
        horizontal,
        vertical
    }
    private DoorsController selectedDoor;
    private GameObject dummyDoor;
    private Rotation rotation;
    void Start()
    {
        
    }
    void Update()
    {
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 doorPosition = this.transform.position + Vector3.ClampMagnitude(mouseWorldPosition - this.transform.position, maxReach);
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (selectedDoor == null)
            {
                rotation = Rotation.horizontal;
                var doorFromInventory = PlayerDoorInventory.Instance.GetBlueDoors();
                if (doorFromInventory != null)
                {
                    selectedDoor = doorFromInventory;
                    dummyDoor = DoorsSpawnManager.Instance.CreateDummyDoor(DoorColor.Blue).gameObject;
                    dummyDoor.SetActive(true);
                }
            }
            else
            {
                selectedDoor = null;
                //PlayerDoorInventory.Instance.AddDoorToPlayerInventory(selectedDoor);
                Destroy(dummyDoor);
            }
        }
        if (selectedDoor != null && dummyDoor != null)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                if (rotation == Rotation.horizontal)
                {
                    rotation = Rotation.vertical;
                    dummyDoor.transform.rotation = Quaternion.Euler(0, 0, 90);
                }
                else
                {
                    rotation = Rotation.horizontal;
                    dummyDoor.transform.rotation = Quaternion.Euler(0, 0, 0);

                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                Destroy(dummyDoor);
                PlaceDoor(selectedDoor, doorPosition, rotation);
                selectedDoor = null;
            }

            dummyDoor.transform.position = doorPosition;
        }
    }
    private void PlaceDoor(DoorsController door, Vector3 position, Rotation rotation)
    {
        door.gameObject.SetActive(true);
        if (rotation == Rotation.horizontal)
        {
            EventManager.Instance.OnPlaceDoorHorizontally(door, position);
        }
        else
        {
            EventManager.Instance.OnPlaceDoorVertically(door, position);
        }
    }
}
