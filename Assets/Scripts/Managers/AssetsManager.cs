using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetsManager : MonoBehaviour
{
    public GameObject redUIDoors;
    public GameObject greenUIDoors;
    public GameObject blueUIDoors;
    public GameObject purpleUIDoors;


    public GameObject redDoors;
    public GameObject greenDoors;
    public GameObject blueDoors;
    public GameObject purpleDoors;

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
