using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetsManager : MonoBehaviour
{
    [SerializeField] public GameObject redUIDoors;
    [SerializeField] public GameObject greenUIDoors;
    [SerializeField] public GameObject blueUIDoors;
    [SerializeField] public GameObject purpleUIDoors;


    [SerializeField] public GameObject redDoors;
    [SerializeField] public GameObject greenDoors;
    [SerializeField] public GameObject blueDoors;
    [SerializeField] public GameObject purpleDoors;

    public GameObject GetUIDoorsPrefab(DoorColor color)
    {
        if (color == DoorColor.Red)
        {
            return redUIDoors;
        }
        else if (color == DoorColor.Green)
        {
            return greenUIDoors;
        }
        else if (color == DoorColor.Blue)
        {
            return blueUIDoors;
        }
        else if (color == DoorColor.Purple)
        {
            return purpleUIDoors;
        }
        else
        {
            return null;
        }
    }
    public GameObject GetDoorsPrefab(DoorColor color)
    {
        if (color == DoorColor.Red)
        {
            return redDoors;
        }
        else if (color == DoorColor.Green)
        {
            return greenDoors;
        }
        else if (color == DoorColor.Blue)
        {
            return blueDoors;
        }
        else if (color == DoorColor.Purple)
        {
            return purpleDoors;
        }
        else
        {
            return null;
        }
    }





    public static AssetsManager Instance { get; private set; }
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
}
