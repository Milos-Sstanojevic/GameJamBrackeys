using UnityEngine;
using UnityEngine.UI;

public class UIDoorInvertoryController : MonoBehaviour
{
    [SerializeField] private DoorColor doorColor;

    public DoorColor GetDoorColor() => doorColor;
}
