using System;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class DoorsSpawnManager : MonoBehaviour
{
    private List<DoorsController> doors;

    [SerializeField] private List<DoorColor> doorsPlayerStartsWith;
    [SerializeField] private List<DoorsController> doorsInScene;

    public static DoorsSpawnManager Instance { get; private set; }
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

        doors = new List<DoorsController>();
    }
    private void OnEnable()
    {
        EventManager.Instance.SubscribeToCollectToken(CollectToken);
        EventManager.Instance.SubscribeToPickUpDoor(PickUpDoor);
    }
    private void OnDisable()
    {
        EventManager.Instance.UnsubscribeToCollectToken(CollectToken);
        EventManager.Instance.UnsubscribeToPickUpDoor(PickUpDoor);
    }
    private void Start()
    {
        foreach (DoorsController door in doorsInScene)
        {
            doors.Add(door);
        }

        foreach (DoorColor color in doorsPlayerStartsWith)
        {
            CollectToken(color);
        }
    }
    public List<DoorsController> GetActiveDoorsOfColor(DoorColor color)
    {
        return doors.FindAll(d => d.gameObject.activeSelf && d.DoorColor == color);
    }
    private void CollectToken(DoorColor color)
    {
        DoorsController doorsController = CreateDoorWithController(color);

        doors.Add(doorsController);
        EventManager.Instance.OnPickedUpDoor(doorsController);
    }
    private void PickUpDoor(DoorsController doorsController)
    {
        doorsController.gameObject.SetActive(false);
        EventManager.Instance.OnPickedUpDoor(doorsController);
    }
    private DoorsController CreateDoorWithController(DoorColor color)
    {
        GameObject door = Instantiate(AssetsManager.Instance.GetDoorsPrefab(color));

        DoorsController doorsController = door.AddComponent<DoorsController>();
        doorsController.DoorColor = color;
        doorsController.IsPremadeInScene = false;

        return doorsController;
    }
    public GameObject CreateDummyDoor(DoorColor color)
    {
        return Instantiate(AssetsManager.Instance.GetDoorsPrefab(color));
    }
}
