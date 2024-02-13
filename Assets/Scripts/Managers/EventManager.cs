using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance { get; private set; }

    private event Action<DoorsController, Vector3> placeDoorHorizontallyAction;
    private event Action<DoorsController, Vector3> placeDoorVerticallyAction;
    private event Action<DoorsController> collectDoorAction;
    private event Action<DoorsController, PlayerController> teleportPlayerAction;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void SubscribeToTeleportPlayer(Action<DoorsController, PlayerController> action)
    {
        teleportPlayerAction += action;
    }

    public void SubscribeToCollectDoorAction(Action<DoorsController> action)
    {
        collectDoorAction += action;
    }

    public void SubscribeToPlaceDoorHorizontallyAction(Action<DoorsController, Vector3> action)
    {
        placeDoorHorizontallyAction += action;
    }

    public void SubscribeToPlaceDoorVerticallyAction(Action<DoorsController, Vector3> action)
    {
        placeDoorVerticallyAction += action;
    }

    public void UnsubscribeToPlaceDoorHorizontallyAction(Action<DoorsController, Vector3> action)
    {
        placeDoorHorizontallyAction -= action;
    }

    public void UnsubscribeToPlaceDoorVerticallyAction(Action<DoorsController, Vector3> action)
    {
        placeDoorVerticallyAction -= action;
    }

    public void UnsubscribeToTeleportPlayer(Action<DoorsController, PlayerController> action)
    {
        teleportPlayerAction -= action;
    }

    public void OnPlaceDoorHorizontally(DoorsController door, Vector3 position)
    {
        placeDoorHorizontallyAction?.Invoke(door, position);
    }

    public void OnPlaceDoorVertically(DoorsController door, Vector3 position)
    {
        placeDoorVerticallyAction?.Invoke(door, position);
    }

    public void OnCollectDoor(DoorsController door)
    {
        collectDoorAction?.Invoke(door);
    }

    public void OnTeleportPlayer(DoorsController door, PlayerController player)
    {
        teleportPlayerAction?.Invoke(door, player);
    }
}