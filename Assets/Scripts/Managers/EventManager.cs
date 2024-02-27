using System;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance { get; private set; }

    private event Action collectJumpsAdder;
    private event Action collectWallJump;

    private event Action<DoorColor> collectToken;

    private event Action<DoorsController> pickUpDoor;
    private event Action<DoorsController> pickedUpDoor;

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





    public void SubscribeToPickUpDoor(Action<DoorsController> action)
    {
        pickUpDoor += action;
    }
    public void UnsubscribeToPickUpDoor(Action<DoorsController> action)
    {
        pickUpDoor -= action;
    }
    public void OnPickUpDoor(DoorsController doorsController)
    {
        pickUpDoor?.Invoke(doorsController);
    }





    public void SubscribeToPickedUpDoor(Action<DoorsController> action)
    {
        pickedUpDoor += action;
    }
    public void UnsubscribeToPickedUpDoor(Action<DoorsController> action)
    {
        pickedUpDoor -= action;
    }
    public void OnPickedUpDoor(DoorsController doorsController)
    {
        pickedUpDoor?.Invoke(doorsController);
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
}
