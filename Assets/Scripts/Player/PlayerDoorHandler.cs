using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Overlays;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerDoorHandler : MonoBehaviour
{
    [SerializeField] float maxReach;
    [SerializeField] GameObject reachIndicator;
    [SerializeField] [Range(0f, 1f)] float dummyDoorOkayOpacity;
    [SerializeField] [Range(0f, 1f)] float dummyDoorInvalidOpacity;
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

    Vector3 mouseWorldPosition;
    Vector3 doorPosition;

    private DoorsController currentlyTouchingDoor;
    void OnEnable()
    {
        EventManager.Instance.SubscribeToSelectedDoor(OnSelectedDoor);
    }
    private void OnDisable()
    {
        EventManager.Instance.UnsubscribeToSelectedDoor(OnSelectedDoor);
    }
    void Update()
    {
        mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0;

        if (selectedDoor != null)
        {
            HandleAddingDoor();
        }
        HandleCollectingDoor();
        HandleTeleporting();
    }
    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawSphere(doorPosition, .1f);
    //    Gizmos.DrawSphere(mouseWorldPosition, .1f);

    //    Debug.Log("m: " + mouseWorldPosition + " tp: " + this.transform.position);


    //    var unclamped = mouseWorldPosition - myPos;
    //    var clamped = Vector3.ClampMagnitude(mouseWorldPosition - myPos, maxReach);
    //    var my = unclamped.magnitude <= maxReach ? unclamped : unclamped.normalized * maxReach;

    //    Debug.Log(unclamped.magnitude + " njihovo: " + clamped.magnitude + " moje: " + my.magnitude);

    //    Gizmos.DrawLine(myPos, myPos + clamped);
    //}
    private void HandleAddingDoor()
    {

        if (reachIndicator.activeSelf == false)
        {
            reachIndicator.SetActive(true);
        }

        var position = this.transform.position;
        position.z = 0;
        

        doorPosition = this.transform.position + Vector3.ClampMagnitude(mouseWorldPosition - position, maxReach);

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
                reachIndicator.SetActive(false);
            }
        }
    }
    public void OnSelectedDoor(DoorsController door)
    {
        if (door == null) throw new Exception("Poslat mi je null umesto vrata");

        if (selectedDoor == door)
        {
            OnDeselectedDoor();
            return;
        }

        if (selectedDoor != null)
        {
            selectedDoor = null;
            Destroy(dummyDoor);
            reachIndicator.SetActive(false);
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
        reachIndicator.SetActive(false);
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

    private void HandleCollectingDoor()
    {
        if (Input.GetKeyDown(KeyCode.Q) && currentlyTouchingDoor != null && !currentlyTouchingDoor.IsDoorPremadeInScene())
        {
            EventManager.Instance.OnCollectDoor(currentlyTouchingDoor);
        }
    }

    private void HandleTeleporting()
    {
        if (Input.GetKeyDown(KeyCode.T) && currentlyTouchingDoor != null)
        {
            EventManager.Instance.OnTeleportPlayer(currentlyTouchingDoor, gameObject);
        }
    }

    public void OnDoorCollisionDetected(DoorsController door)
    {
        currentlyTouchingDoor = door;
    }

    public void OnDoorCollisionEnd()
    {
        currentlyTouchingDoor = null;
    }
}
