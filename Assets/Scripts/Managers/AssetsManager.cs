using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetsManager : MonoBehaviour
{
    [SerializeField] GameObject DummyDoor;
    [SerializeField] List<GameObject> UIDoors;

    public GameObject GetUIDoorsPrefab(DoorColor color)
    {
        return UIDoors.Find(d => d.name == color.ToString());
    }
    public GameObject GetColoredDummy(DoorColor color)
    {
        var go = Instantiate(DummyDoor);
        go.GetComponent<SpriteRenderer>().color = GetRGB(color);

        return go;
    }
    public static Color GetRGB(DoorColor color)
    {
        switch (color)
        {
            case DoorColor.Red:
                return new Color(1f, 0f, 0f);
            case DoorColor.Green:
                return new Color(.4f, 1f, 0f);
            case DoorColor.Blue:
                return new Color(0f, .77f, 1f);
            case DoorColor.Purple:
                return new Color(.54f, .17f, .89f);
            case DoorColor.Pink:
                return new Color(1f, .57f, .69f);
            case DoorColor.Orange:
                return new Color(1f, .55f, 0f);
            case DoorColor.Yellow:
                return new Color(1f, .94f, 0f);
            default:
                return Color.black;
        }
    }

    //public GameObject redUIDoors;
    //public GameObject greenUIDoors;
    //public GameObject blueUIDoors;
    //public GameObject purpleUIDoors;


    //public GameObject redDoors;
    //public GameObject greenDoors;
    //public GameObject blueDoors;
    //public GameObject purpleDoors;

    //public GameObject GetUIDoorsPrefab(DoorColor color)
    //{
    //    switch (color)
    //    {
    //        case DoorColor.Red:
    //            return redUIDoors;
    //        case DoorColor.Green:
    //            return greenUIDoors;
    //        case DoorColor.Blue:
    //            return blueUIDoors;
    //        case DoorColor.Purple:
    //            return purpleUIDoors;
    //        case DoorColor.Pink:
    //            return null;
    //        case DoorColor.Orange:
    //            return null;
    //        case DoorColor.Yellow:
    //            return null;
    //        default: 
    //            return null;
    //    }
    //}

    //public GameObject GetDoorsPrefab(DoorColor color)
    //{
    //    switch (color)
    //    {
    //        case DoorColor.Red:
    //            return redDoors;
    //        case DoorColor.Green:
    //            return greenDoors;
    //        case DoorColor.Blue:
    //            return blueDoors;
    //        case DoorColor.Purple:
    //            return purpleDoors;
    //        case DoorColor.Pink:
    //            return null;
    //        case DoorColor.Orange:
    //            return null;
    //        case DoorColor.Yellow:
    //            return null;
    //        default:
    //            return null;
    //    }
    //}





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
