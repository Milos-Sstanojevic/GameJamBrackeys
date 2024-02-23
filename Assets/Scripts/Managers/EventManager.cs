using System;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance { get; private set; }

    private event Action collectJumpsAdder;
    private event Action collectWallJump;

    private event Action<DoorColor> collectToken;

    private event Action<DoorsController> collectDoor;
    private event Action<DoorsController> collectedDoor;

    private event Action<GameObject> playerTeleported;

    private event Action<int> leftJumpsChanged;
    private event Action<int> healthChanged;
    private event Action<int> keysCountChanged;

    private event Action playerDied;

    private event Action<DoorsController> selectDoor;
    private event Action<DoorsController> placeDoor;

    private event Action<PlatformsControlledByLevers> onChangeDirection;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void SubscribeToCollectJumpsAdder(Action action)
    {
        collectJumpsAdder += action;
    }
    public void UnsubscribeToCollectJumpsAdder(Action action)
    {
        collectJumpsAdder -= action;
    }
    public void OnCollectJumpsAdder()
    {
        collectJumpsAdder?.Invoke();
    }




    public void SubscribeToCollectWallJump(Action action)
    {
        collectWallJump += action;
    }
    public void UnsubscribeToCollectWallJump(Action action)
    {
        collectWallJump -= action;
    }
    public void OnCollectWallJump()
    {
        collectWallJump?.Invoke();
    }





    public void SubscribeToCollectToken(Action<DoorColor> action)
    {
        collectToken += action;
    }
    public void UnsubscribeToCollectToken(Action<DoorColor> action)
    {
        collectToken -= action;
    }
    public void OnCollectToken(DoorColor color)
    {
        collectToken?.Invoke(color);
    }





    public void SubscribeToCollectDoor(Action<DoorsController> action)
    {
        collectDoor += action;
    }
    public void UnsubscribeToCollectDoor(Action<DoorsController> action)
    {
        collectDoor -= action;
    }
    public void OnCollectDoor(DoorsController doorsController)
    {
        collectDoor?.Invoke(doorsController);
    }





    public void SubscribeToCollectedDoor(Action<DoorsController> action)
    {
        collectedDoor += action;
    }
    public void UnsubscribeToCollectedDoor(Action<DoorsController> action)
    {
        collectedDoor -= action;
    }
    public void OnCollectedDoor(DoorsController doorsController)
    {
        collectedDoor?.Invoke(doorsController);
    }





    public void SubscribeToPlayerTeleported(Action<GameObject> action)
    {
        playerTeleported += action;
    }
    public void UnsubscribeToPlayerTeleported(Action<GameObject> action)
    {
        playerTeleported -= action;
    }
    public void OnPlayerTeleported(GameObject player)
    {
        playerTeleported?.Invoke(player);
    }





    public void SubscribeToLeftJumpsChanged(Action<int> action)
    {
        leftJumpsChanged += action;
    }
    public void UnsubscribeToLeftJumpsChanged(Action<int> action)
    {
        leftJumpsChanged -= action;
    }
    public void OnLeftJumpsChanged(int leftJumps)
    {
        leftJumpsChanged?.Invoke(leftJumps);
    }





    public void SubscribeToHealthChanged(Action<int> action)
    {
        healthChanged += action;
    }
    public void UnsubscribeToHealthChanged(Action<int> action)
    {
        healthChanged -= action;
    }
    public void OnHealthChanged(int health)
    {
        healthChanged?.Invoke(health);
    }





    public void SubscribeToKeysCountChanged(Action<int> action)
    {
        keysCountChanged += action;
    }
    public void UnsubscribeToKeysCountChanged(Action<int> action)
    {
        keysCountChanged -= action;
    }
    public void OnKeysCountChanged(int count)
    {
        keysCountChanged?.Invoke(count);
    }





    public void SubscribeToPlayerDied(Action action)
    {
        playerDied += action;
    }
    public void UnsubscribeToPlayerDied(Action action)
    {
        playerDied -= action;
    }
    public void OnPlayerDied()
    {
        playerDied?.Invoke();
    }




    public void SubscribeToSelectDoor(Action<DoorsController> action)
    {
        selectDoor += action;
    }
    public void UnsubscribeToSelectDoor(Action<DoorsController> action)
    {
        selectDoor -= action;
    }
    public void OnSelectDoor(DoorsController doorsController)
    {
        selectDoor?.Invoke(doorsController);
    }




    public void SubscribeToPlaceDoor(Action<DoorsController> action)
    {
        placeDoor += action;
    }
    public void UnsubscribeToPlaceDoor(Action<DoorsController> action)
    {
        placeDoor -= action;
    }
    public void OnPlaceDoor(DoorsController doorsController)
    {
        placeDoor?.Invoke(doorsController);
    }





    public void SubscribeToOnChangeDirection(Action<PlatformsControlledByLevers> action)
    {
        onChangeDirection += action;
    }
    public void UnsubscribeToOnChangeDirection(Action<PlatformsControlledByLevers> action)
    {
        onChangeDirection -= action;
    }
    public void OnChangeDirection(PlatformsControlledByLevers platform)
    {
        onChangeDirection?.Invoke(platform);
    }





    //public void SubscribeToCollectedWallJump(Action action)
    //{
    //    collectedWallJump += action;
    //}
    //public void SubscribeToCollectedJumpsAdder(Action action)
    //{
    //    collectedJumpsAdder += action;
    //}
    //public void SubscribeToKeysCountChanged(Action<int> action)
    //{
    //    keysCountChanged += action;
    //}
    //public void SubscribeToPlayerDied(Action action)
    //{
    //    playerDied += action;
    //}
    //public void SubscribeToSelectedDoor(Action<DoorsController> action)
    //{
    //    selectedDoor += action;
    //}
    //public void SubscribeToTakeDoorFromInventoryAction(Action<DoorsController> action)
    //{
    //    takeDoorFromInventoryAction += action;
    //}

    //public void SubscribeToTokenCollected(Action<DoorColor> action)
    //{
    //    tokenCollected += action;
    //}

    //public void SubscribeToTeleportPlayer(Action<DoorsController, GameObject> action)
    //{
    //    teleportPlayerAction += action;
    //}

    //public void SubscribeToCollectedDoor(Action<DoorsController> action)
    //{
    //    collectedDoor += action;
    //}
    //public void UnsubscribeToTeleportationFinished(Action<GameObject> action)
    //{
    //    teleportedPlayer -= action;
    //}
    //public void UnsubscribeToCollectedWallJump(Action action)
    //{
    //    collectedWallJump -= action;
    //}
    //public void UnsubscribeToCollectedJumpsAdder(Action action)
    //{
    //    collectedJumpsAdder -= action;
    //}
    //public void UnsubscribeToPlayerDied(Action action)
    //{
    //    playerDied -= action;
    //}
    //public void UnsubscribeToSelectedDoor(Action<DoorsController> action)
    //{
    //    selectedDoor -= action;
    //}
    //public void UnsubscribeToTeleportPlayer(Action<DoorsController, GameObject> action)
    //{
    //    teleportPlayerAction -= action;
    //}

    //public void UnsubscribeToTokenCollected(Action<DoorColor> action)
    //{
    //    tokenCollected -= action;
    //}

    //public void UnsubscribeToTakeDoorFromInventoryAction(Action<DoorsController> action)
    //{
    //    takeDoorFromInventoryAction -= action;
    //}

    //public void UnsubscribeToCollectedDoor(Action<DoorsController> action)
    //{
    //    collectedDoor -= action;
    //}
    //public void OnTeleportationFinished(GameObject obj)
    //{
    //    teleportedPlayer?.Invoke(obj);
    //}
    //public void OnCollectedWallJump()
    //{
    //    collectedWallJump?.Invoke();
    //}
    //public void OnCollectedJumpsAdder()
    //{
    //    collectedJumpsAdder?.Invoke();
    //}
    //public void OnKeysCountChanged(int count)
    //{
    //    keysCountChanged?.Invoke(count);
    //}
    //public void OnPlayerDied()
    //{
    //    playerDied?.Invoke();
    //}
    //public void OnSelectedDoor(DoorsController door)
    //{
    //    selectedDoor?.Invoke(door);
    //}
    //public void OnCollectedDoor(DoorsController door)
    //{
    //    collectedDoor?.Invoke(door);
    //}

    //public void OnTeleportPlayer(DoorsController door, GameObject player)
    //{
    //    teleportPlayerAction?.Invoke(door, player);
    //}

    //public void TokenCollected(DoorColor color)
    //{
    //    tokenCollected?.Invoke(color);
    //}

    //public void OnTakeDoor(DoorsController door)
    //{
    //    takeDoorFromInventoryAction?.Invoke(door);
    //}

}
