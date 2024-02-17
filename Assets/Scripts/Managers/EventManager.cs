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
    private event Action<DoorsController> takeDoorFromInventoryAction;
    private event Action<DoorsController> selectedDoor;
    private event Action playerDied;
    private event Action<PlatformsControlledByLevers> onChangeDirection;
    private event Action<int> leftJumpsChanged;
    private event Action<int> healthChanged;
    private event Action<int> keysCountChanged;
    private event Action collectedJumpsAdder;
    private event Action collectedWallJump;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void SubscribeToCollectedWallJump(Action action)
    {
        collectedWallJump += action;
    }
    public void SubscribeToCollectedJumpsAdder(Action action)
    {
        collectedJumpsAdder += action;
    }
    public void SubscribeToOnChangeDirection(Action<PlatformsControlledByLevers> action)
    {
        onChangeDirection += action;
    }
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
    }
    public void SubscribeToPlayerDied(Action action)
    {
        playerDied += action;
    }
    public void SubscribeToSelectedDoor(Action<DoorsController> action)
    {
        selectedDoor += action;
    }
    public void SubscribeToTakeDoorFromInventoryAction(Action<DoorsController> action)
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

    public void UnsubscribeToCollectedWallJump(Action action)
    {
        collectedWallJump -= action;
    }
    public void UnsubscribeToCollectedJumpsAdder(Action action)
    {
        collectedJumpsAdder -= action;
    }
    public void UnsubscribeToOnChangeDirection(Action<PlatformsControlledByLevers> action)
    {
        onChangeDirection -= action;
    }
    public void UnsubscribeToLeftJumpsChanged(Action<int> action)
    {
        leftJumpsChanged -= action;
    }
    public void UnsubscribeToPlayerDied(Action action)
    {
        playerDied -= action;
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

    public void UnsubscribeToTakeDoorFromInventoryAction(Action<DoorsController> action)
    {
        takeDoorFromInventoryAction -= action;
    }

    public void UnsubscribeToCollectDoorAction(Action<DoorsController> action)
    {
        collectDoorAction -= action;
    }
    public void OnCollectedWallJump()
    {
        collectedWallJump?.Invoke();
    }
    public void OnCollectedJumpsAdder()
    {
        collectedJumpsAdder?.Invoke();
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

    public void OnTakeDoor(DoorsController door)
    {
        takeDoorFromInventoryAction?.Invoke(door);
    }

    public void OnChangeDirection(PlatformsControlledByLevers platform)
    {
        onChangeDirection?.Invoke(platform);
    }
}
