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
                instantiatedDoors.Add(Instantiate(d,new Vector3(30,-30,0),Quaternion.identity));
                d.transform.SetParent(canvas.transform);
            }
        }
    }

    private void UITake(DoorColor doorColor)
    {
        foreach(UIDoorInvertoryController d in instantiatedDoors)
        {
            if(d.GetDoorColor() == doorColor)
            {
                Destroy(d);
            }
        }
    }

    private void OnDisable()
    {
        EventManager.Instance.UnsubscribeToCollectDoorAction(UIAdd);
        EventManager.Instance.UnsubscribeToTakeDoorFromInventoryAction(UITake);
    }
}
