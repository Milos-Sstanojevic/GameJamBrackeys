using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerDoorHandler : MonoBehaviour
{
    [SerializeField] float maxReach;
    [SerializeField] [Range(0f, 1f)] float dummyDoorOkayOpacity;
    [SerializeField][Range(0f, 1f)] float dummyDoorInvalidOpacity;
    [SerializeField] Color invalidColor;
    private enum Rotation
    {
        horizontal,
        vertical
    }
    private DoorsController selectedDoor;
    private Color selectedDoorStartingColor;
    private GameObject dummyDoor;
    private SpriteRenderer dummyDoorSpriteRenderer;
    private Collider2D dummyDoorCollider;
    private Rotation rotation;
    void Start()
    {
        EventManager.Instance.SubscribeToSelectedDoor(OnSelectedDoor);
        EventManager.Instance.SubscribeToDeselectedDoor(OnDeselectedDoor);
    }
    void Update()
    {
        if (selectedDoor == null) return;

        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 doorPosition = this.transform.position + Vector3.ClampMagnitude(mouseWorldPosition - this.transform.position, maxReach);

        if (selectedDoor != null && dummyDoor != null)
        {
            var isOverlaping = dummyDoorCollider.OverlapCollider(new ContactFilter2D().NoFilter(), new Collider2D[1]) > 0;

            var dir = doorPosition - this.transform.position;
            var hits = Physics2D.RaycastAll(transform.position, dir.normalized, dir.magnitude);

            var isObstructed = hits.Any(hit => hit.collider.CompareTag("Platform"));

            var canPlaceDoor = !(isOverlaping || isObstructed);

            var newColor = selectedDoorStartingColor;
            newColor.a = canPlaceDoor ? dummyDoorOkayOpacity : dummyDoorInvalidOpacity;
            dummyDoorSpriteRenderer.color = newColor;

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

            dummyDoor.transform.position = doorPosition;

            if (canPlaceDoor && Input.GetMouseButtonUp(0))
            {
                PlaceDoor(selectedDoor, doorPosition, rotation);
                Destroy(dummyDoor);
                selectedDoor = null;
                dummyDoor = null;
            }
        }
    }
    public void OnSelectedDoor(DoorsController door)
    {
        //var door = PlayerDoorInventory.Instance.GetBlueDoors();
        if (door == null)
        {
            throw new Exception("Poslat mi je null umesto vrata");
        }

        if (selectedDoor != null)
        {
            selectedDoor = null;
            Destroy(dummyDoor);
        }

        selectedDoor = door;

        dummyDoor = DoorsSpawnManager.Instance.CreateDummyDoor(door.GetDoorColor());
        dummyDoor.SetActive(true);
        dummyDoor.tag = "Untagged";

        dummyDoorSpriteRenderer = dummyDoor.GetComponent<SpriteRenderer>();
        selectedDoorStartingColor = dummyDoorSpriteRenderer.color;

        dummyDoorCollider = dummyDoor.GetComponent<Collider2D>();
        dummyDoorCollider.isTrigger = true;

        rotation = Rotation.horizontal;
    }
    public void OnDeselectedDoor()
    {
        selectedDoor = null;
        Destroy(dummyDoor);
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
        PlayerDoorInventory.Instance.TakeDoorFromInventory(door);
    }
}
