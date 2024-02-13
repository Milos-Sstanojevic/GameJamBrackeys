using UnityEngine;
using UnityEngine.UI;

public class UIDoorInvertoryController : MonoBehaviour
{
    [SerializeField] private Slider sliderRed;
    [SerializeField] private Slider sliderGreen;
    [SerializeField] private Slider sliderBlue;

    private void OnEnable()
    {
        EventManager.Instance.SubscribeToCreateDoorAction(AddDoor);
        EventManager.Instance.SubscribeToTakeDoorFromInventoryAction(TakeDoor);
    }

    private void AddDoor(DoorColor color)
    {
        switch (color)
        {
            case DoorColor.Red:
                {
                    AddRed();
                    break;
                }
            case DoorColor.Blue:
                {
                    AddBlue();
                    break;
                }
            case DoorColor.Green:
                {
                    AddGreen();
                    break;
                }
        }
    }

    private void TakeDoor(DoorColor color)
    {
        switch (color)
        {
            case DoorColor.Red:
                {
                    TakeRed();
                    break;
                }
            case DoorColor.Blue:
                {
                    TakeBlue();
                    break;
                }
            case DoorColor.Green:
                {
                    TakeGreen();
                    break;
                }
        }
    }

    private void TakeRed()
    {
        sliderRed.value--;
        if (sliderRed.value <= 0)
        {
            sliderRed.value = 0;
        }
    }

    private void TakeGreen()
    {
        sliderGreen.value--;
        if (sliderGreen.value <= 0)
        {
            sliderGreen.value = 0;
        }
    }

    private void TakeBlue()
    {
        sliderBlue.value--;
        if (sliderBlue.value <= 0)
        {
            sliderBlue.value = 0;
        }
    }

    private void AddRed()
    {
        sliderRed.value++;
        if (sliderRed.value >= 2)
        {
            sliderRed.value = 2;
        }
    }

    private void AddBlue()
    {
        sliderBlue.value++;
        if (sliderBlue.value >= 2)
        {
            sliderBlue.value = 2;
        }
    }

    private void AddGreen()
    {
        sliderGreen.value++;
        if (sliderGreen.value >= 2)
        {
            sliderGreen.value = 2;
        }
    }
}
