using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance { get; private set; }

    private event Action<DoorsController, Vector3> placeDoorHorizontallyAction;
    private event Action<DoorsController, Vector3> placeDoorVerticallyAction;
    private event Action<DoorsController> collectDoorAction;
    private event Action<DoorsController, GameObject> teleportPlayerAction;
    private event Action<DoorColor> createDoorAction;
    private event Action<DoorColor> takeDoorFromInventoryAction;
    private event Action<DoorsController> selectedDoor;
    private event Action deselectedDoor;
    private event Action playerDied;
<<<<<<< HEAD
    private event Action<PlatformsControlledByLevers> onChangeDirection;
=======
    private event Action<int> leftJumpsChanged;
    private event Action<int> healthChanged;
    private event Action<int> keysCountChanged;
>>>>>>> e4182f6fa4908681f4e8363defcfef57463d1a12

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
<<<<<<< HEAD
    
    public void SubscribeToOnChangeDirection(Action<PlatformsControlledByLevers> action)
    {
        onChangeDirection += action;
=======
    public void SubscribeToHealthChanged(Action<int> action)
    {
        healthChanged += action;
    }
    public void SubscribeToKeysCountChanged(Action<int> action)
    {
        keysCountChanged += action;
    }
    public void SubscribeToLeftJumpsChanged(Action<int> action)
    {
        leftJumpsChanged += action;
>>>>>>> e4182f6fa4908681f4e8363defcfef57463d1a12
    }
    public void SubscribeToPlayerDied(Action action)
    {
        playerDied += action;
    }
    public void SubscribeToDeselectedDoor(Action action)
    {
        deselectedDoor += action;
    }
    public void SubscribeToSelectedDoor(Action<DoorsController> action)
    {
        selectedDoor += action;
    }
    public void SubscribeToTakeDoorFromInventoryAction(Action<DoorColor> action)
    {
        takeDoorFromInventoryAction += action;
    }

    public void SubscribeToCreateDoorAction(Action<DoorColor> action)
    {
        createDoorAction += action;
    }

    public void SubscribeToTeleportPlayer(Action<DoorsController, GameObject> action)
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
<<<<<<< HEAD

    public void UnsubscribeToOnChangeDirection(Action<PlatformsControlledByLevers> action)
    {
        onChangeDirection -= action;
=======
    public void UnsubscribeToLeftJumpsChanged(Action<int> action)
    {
        leftJumpsChanged -= action;
>>>>>>> e4182f6fa4908681f4e8363defcfef57463d1a12
    }
    public void UnsubscribeToPlayerDied(Action action)
    {
        playerDied -= action;
    }
    public void UnsubscribeToDeselectedDoor(Action action)
    {
        deselectedDoor -= action;
    }
    public void UnsubscribeToSelectedDoor(Action<DoorsController> action)
    {
        selectedDoor -= action;
    }
    public void UnsubscribeToPlaceDoorHorizontallyAction(Action<DoorsController, Vector3> action)
    {
        placeDoorHorizontallyAction -= action;
    }

    public void UnsubscribeToPlaceDoorVerticallyAction(Action<DoorsController, Vector3> action)
    {
        placeDoorVerticallyAction -= action;
    }

    public void UnsubscribeToTeleportPlayer(Action<DoorsController, GameObject> action)
    {
        teleportPlayerAction -= action;
    }

    public void UnsubscribeToCreateDoorAction(Action<DoorColor> action)
    {
        createDoorAction -= action;
    }

    public void UnsubscribeToTakeDoorFromInventoryAction(Action<DoorColor> action)
    {
        takeDoorFromInventoryAction -= action;
    }

    public void UnsubscribeToCollectDoorAction(Action<DoorsController> action)
    {
        collectDoorAction -= action;
    }
    public void OnKeysCountChanged(int count)
    {
        keysCountChanged?.Invoke(count);
    }
    public void OnHealthChanged(int health)
    {
        healthChanged?.Invoke(health);
    }
    public void OnLeftJumpsChanged(int leftJumps)
    {
        leftJumpsChanged?.Invoke(leftJumps);
    }
    public void OnPlayerDied()
    {
        playerDied?.Invoke();
    }
    public void OnDeselectedDoor()
    {
        deselectedDoor?.Invoke();
    }
    public void OnSelectedDoor(DoorsController door)
    {
        selectedDoor?.Invoke(door);
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

    public void OnTeleportPlayer(DoorsController door, GameObject player)
    {
        teleportPlayerAction?.Invoke(door, player);
    }

    public void OnCreateDoor(DoorColor color)
    {
        createDoorAction?.Invoke(color);
    }

    public void OnTakeDoor(DoorColor color)
    {
        takeDoorFromInventoryAction?.Invoke(color);
    }

    public void OnChangeDirection(PlatformsControlledByLevers platform)
    {
        onChangeDirection?.Invoke(platform);
    }
}
