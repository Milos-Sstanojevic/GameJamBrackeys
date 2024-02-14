using System.Collections.Generic;
using UnityEngine;

public class DoorsSpawnManager : MonoBehaviour
{
    public static DoorsSpawnManager Instance { get; private set; }
    private List<DoorsController> teleportAvailableDoors;
    [SerializeField] private List<DoorsController> doorsPlayerStartsWith;
    [SerializeField] private List<DoorsController> doorsInScene;
    [SerializeField] private DoorsController greenDoorPrefab;
    [SerializeField] private DoorsController redDoorsPrefab;
    [SerializeField] private DoorsController blueDoorsPrefab;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        teleportAvailableDoors = new List<DoorsController>();
    }

    private void OnEnable()
    {
        EventManager.Instance.SubscribeToCreateDoorAction(CreateDoor);
    }

    private void Start()
    {
        InitializePlayerInventory();
        InitializeTeleportAvailableDoors();
    }

    private void InitializeTeleportAvailableDoors()
    {
        foreach (DoorsController door in doorsInScene)
            teleportAvailableDoors.Add(door);
    }

    private void InitializePlayerInventory()
    {
        foreach (DoorsController door in doorsPlayerStartsWith)
        {
            DoorsController d = Instantiate(door);
            PlayerDoorInventory.Instance.AddDoorToPlayerInventory(d);
            teleportAvailableDoors.Add(d);
        }
    }

    public List<DoorsController> GetActiveDoorsOfColor(DoorColor color)
    {
        List<DoorsController> doors = new List<DoorsController>();
        foreach (DoorsController door in teleportAvailableDoors)
        {
            if (door.GetDoorColor() == color && door.gameObject.activeSelf)
                doors.Add(door);
        }

        if (doors.Count == 2)
            return doors;

        return null;
    }

    public void CreateDoor(DoorColor color)
    {
        if (color == DoorColor.Green)
            PlayerDoorInventory.Instance.AddDoorToPlayerInventory(Instantiate(greenDoorPrefab));
        if (color == DoorColor.Red)
            PlayerDoorInventory.Instance.AddDoorToPlayerInventory(Instantiate(redDoorsPrefab));
        if (color == DoorColor.Blue)
            PlayerDoorInventory.Instance.AddDoorToPlayerInventory(Instantiate(blueDoorsPrefab));
    }
    public GameObject CreateDummyDoor(DoorColor color)
    {
        if (color == DoorColor.Green)
        {
            return Instantiate(greenDoorPrefab).gameObject;
        }
        else if (color == DoorColor.Red)
        {
            return Instantiate(redDoorsPrefab).gameObject;
        }
        else if (color == DoorColor.Blue)
        {
            return Instantiate(blueDoorsPrefab).gameObject;
        }
        else
        {
            return null;
        }
    }
}
