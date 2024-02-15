using UnityEngine;
using UnityEngine.UI;

public class UIDoorInvertoryController : MonoBehaviour
{
    [SerializeField] private DoorColor doorColor;
    [SerializeField] private Slider slider;

    public DoorColor GetDoorColor() => doorColor;
}
