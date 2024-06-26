using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Scripting;
using static Unity.Burst.Intrinsics.X86.Avx;

public class UIDoorManager : MonoBehaviour
{
    [SerializeField] private GameObject parentUI;
    [SerializeField] private GameObject selector;

    private DoorsController selectedDoor;
    private List<DoorsControllerUIWrapper> wrappers;
    private void Awake()
    {
        wrappers = new List<DoorsControllerUIWrapper>();
    }
    private void OnEnable()
    {
        EventManager.Instance.SubscribeToPickedUpDoor(Add);
        EventManager.Instance.SubscribeToPlaceDoor(Remove);
        EventManager.Instance.SubscribeToSelectDoor(OnSelectedDoor);
    }
    private void OnDisable()
    {
        EventManager.Instance.UnsubscribeToPickedUpDoor(Add);
        EventManager.Instance.UnsubscribeToPlaceDoor(Remove);
        EventManager.Instance.UnsubscribeToSelectDoor(OnSelectedDoor);
    }

    private void Add(DoorsController door)
    {
        var visual = AssetsManager.Instance.GetUIDoorsPrefab(door.DoorColor);

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
            DetachSelector();
            wrappers.Remove(wrapper);
            Destroy(wrapper.gameObject);
        }
    }
    private void OnSelectedDoor(DoorsController door)
    {
        if (selectedDoor == door)
        {
            DetachSelector();
            selectedDoor = null;
        }
        else
        {
            var wrapper = wrappers.Find(w => w.doorController == door);
            AttachSelector(wrapper.transform);
            selectedDoor = door;
        }
    }
    private void DetachSelector()
    {
        selector.transform.SetParent(null, false);
        selector.transform.localPosition = Vector3.zero;
        selector.SetActive(false);
    }
    private void AttachSelector(Transform parent)
    {
        selector.transform.SetParent(parent, false);
        selector.transform.localPosition = Vector3.zero;
        selector.SetActive(true);
    }
}
