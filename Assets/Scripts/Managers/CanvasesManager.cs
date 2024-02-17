using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasesManager : MonoBehaviour
{
    [SerializeField] Canvas mainCanvas;
    [SerializeField] Canvas settingsCanvas;

    public void OpenSettings()
    {
        mainCanvas.gameObject.SetActive(false);
        settingsCanvas.gameObject.SetActive(true);
    }
    public void CloseSettings()
    {
        mainCanvas.gameObject.SetActive(true);
        settingsCanvas.gameObject.SetActive(false);
    }
}
