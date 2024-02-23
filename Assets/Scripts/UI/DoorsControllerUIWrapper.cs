using Unity.VisualScripting;
using UnityEngine;
public class DoorsControllerUIWrapper : MonoBehaviour
{
    [DoNotSerialize] public DoorsController doorController;
    public DoorColor GetDoorColor() => doorController.DoorColor;
}
