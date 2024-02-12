using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorInvertoryController : MonoBehaviour
{
    [SerializeField] private Slider sliderRed;
    [SerializeField] private Slider sliderGreen;
    [SerializeField] private Slider sliderBlue;

    private void TakeRed()
    {
        sliderRed.value--;
        if (sliderRed.value<=0)
        {
            sliderRed.value=0;
        }
    }

    private void TakeGreen()
    {
        sliderGreen.value--;
        if (sliderGreen.value<=0)
        {
            sliderGreen.value=0;
        }
    }

    private void TakeBlue()
    {
        sliderBlue.value--;
        if (sliderBlue.value<=0)
        {
            sliderBlue.value=0;
        }
    }

    private void GiveRed()
    {
        sliderRed.value++;
        if (sliderRed.value>=2)
        {
            sliderRed.value=2;
        }
    }

    private void GiveBlue()
    {
        sliderBlue.value++;
        if (sliderBlue.value>=2)
        {
            sliderBlue.value=2;
        }
    }

    private void GiveGreen()
    {
        sliderGreen.value++;
        if (sliderGreen.value>=2)
        {
            sliderGreen.value=2;
        }
    }
}
