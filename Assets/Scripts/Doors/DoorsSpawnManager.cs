using System.Collections.Generic;
using UnityEngine;

public class DoorsSpawnManager : MonoBehaviour
{
    public static DoorsSpawnManager Instance { get; private set; }
    private List<DoorsController> doorsForPlayer;
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

        doorsForPlayer = new List<DoorsController>();
    }

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        foreach (DoorsController door in doorsPlayerStartsWith)
        {
            DoorsController doors = Instantiate(door);
            PlayerDoorInventory.Instance.AddDoorToPlayerInventory(doors);
        }
    }


    public List<DoorsController> GetActiveDoorsOfColor(DoorColor color)
    {
        List<DoorsController> doors = new List<DoorsController>();
        foreach (DoorsController door in doorsForPlayer)
        {
            if (door.GetDoorColor() == color && door.gameObject.active)
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

    public List<DoorsController> GetAllDoorsForPlayer() => doorsForPlayer;
}
