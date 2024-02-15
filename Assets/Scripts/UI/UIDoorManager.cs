using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Scripting;

public class UIDoorManager : MonoBehaviour
{
    [SerializeField] private List<UIDoorInvertoryController> UIDoorPrefabs;
    [SerializeField] private Canvas canvas;
    private List<UIDoorInvertoryController> instantiatedDoors = new List<UIDoorInvertoryController>();
    
    private void Start()
    {
        foreach(DoorsController d in PlayerDoorInventory.Instance.GetDoorsInPlayerInventory())
        {
            UIAdd(d);
        }
    }
    private void OnEnable()
    {
        EventManager.Instance.SubscribeToCollectDoorAction(UIAdd);
        EventManager.Instance.SubscribeToTakeDoorFromInventoryAction(UITake);
    }

    private void UIAdd(DoorsController door)
    {
        foreach(UIDoorInvertoryController d in UIDoorPrefabs)
        {
            if(d.GetDoorColor() == door.GetDoorColor())
            {
                UIDoorInvertoryController doorsUI = Instantiate(d);
                instantiatedDoors.Add(doorsUI);
                doorsUI.transform.SetParent(canvas.transform);
                SetPosition();
            }
        }
    }

    private void SetPosition()
    {
        int i = 0;
        foreach(UIDoorInvertoryController door in instantiatedDoors)
        {
            door.transform.position = new Vector3(-32+i*4,13,0);
            i++;
        }
    }

    private void UITake(DoorColor doorColor)
    {
        UIDoorInvertoryController doorDestroy = null;
        foreach(UIDoorInvertoryController d in instantiatedDoors)
        {
            if(d.GetDoorColor() == doorColor)
            {
                doorDestroy = d;
            }
        }
        if(doorDestroy!=null)
        {
            instantiatedDoors.Remove(doorDestroy);
            Destroy(doorDestroy.gameObject);
            SetPosition();
        }
    }

    private void OnDisable()
    {
        EventManager.Instance.UnsubscribeToCollectDoorAction(UIAdd);
        EventManager.Instance.UnsubscribeToTakeDoorFromInventoryAction(UITake);
    }
}
