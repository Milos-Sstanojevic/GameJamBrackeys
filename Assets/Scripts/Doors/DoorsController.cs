using System.Collections.Generic;
using UnityEngine;

public enum DoorColor
{
    Red,
    Green,
    Blue,
    Purple,
    Pink,
    Orange,
    Yellow
}
public class DoorsController : MonoBehaviour
{
    [SerializeField] public DoorColor DoorColor;
    [SerializeField] public bool IsPremadeInScene;
    public void TeleportPlayer(GameObject player)
    {
        List<DoorsController> doors = DoorsSpawnManager.Instance.GetActiveDoorsOfColor(this.DoorColor);

        if (doors.Count == 2)
        {
            DoorsController other = doors.Find(d => d != this);
            player.transform.position = new Vector3(other.transform.position.x, other.transform.position.y + 0.5f, other.transform.position.z);

            EventManager.Instance.OnPlayerTeleported(player);
        }
    }

    //public DoorColor GetDoorColor() => doorColor;
    //public bool IsDoorPremadeInScene() => isPremadeInScene;


    //private void CollectDoor(DoorsController door)
    //{
    //    if (door == this)
    //    {
    //        door.gameObject.SetActive(false);
    //        PlayerDoorInventory.Instance.AddDoorToPlayerInventory(door);
    //    }
    //}
}
