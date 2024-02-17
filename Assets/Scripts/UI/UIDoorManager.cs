using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Scripting;

public class UIDoorManager : MonoBehaviour
{
    [SerializeField] private GameObject parentUI;

    private List<DoorsControllerUIWrapper> wrappers;
    
    private void Start()
    {
        wrappers = new List<DoorsControllerUIWrapper>();
        foreach (DoorsController controller in PlayerDoorInventory.Instance.GetDoorsInPlayerInventory())
        {
            Add(controller);
        }
    }
    private void OnEnable()
    {
        EventManager.Instance.SubscribeToCollectDoorAction(Add);
        EventManager.Instance.SubscribeToTakeDoorFromInventoryAction(Remove);
        EventManager.Instance.SubscribeToSelectedDoor(OnSelectedDoor);
    }

    private void Add(DoorsController door)
    {
        var visual = AssetsManager.Instance.GetUIDoorsPrefab(door.GetDoorColor());

        if (visual != null)
        {
            var wrapper = Instantiate(visual, parentUI.transform).AddComponent<DoorsControllerUIWrapper>();
            wrapper.doorController = door;
            wrappers.Add(wrapper);
        }
    }

    private void Remove(DoorsController door)
    {
        var wrapper = wrappers.Find(w => w.doorController == door);
        if (wrapper != null)
        {
            wrappers.Remove(wrapper);
            Destroy(wrapper.gameObject);
        }
    }
    private void OnSelectedDoor(DoorsController door)
    {
        Debug.Log("Hajlajtuj ta i ta vrataa");
    }
    private void OnDisable()
    {
        EventManager.Instance.UnsubscribeToCollectDoorAction(Add);
        EventManager.Instance.UnsubscribeToTakeDoorFromInventoryAction(Remove);
    }
}
