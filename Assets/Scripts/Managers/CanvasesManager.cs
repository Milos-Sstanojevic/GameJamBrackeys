using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasesManager : MonoBehaviour
{
    [SerializeField] Canvas mainCanvas;
    [SerializeField] Canvas settingsCanvas;

    public static CanvasesManager Instance;
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
    }

    public void HideAllCanvases()
    {
        mainCanvas.gameObject.SetActive(false);
        settingsCanvas.gameObject.SetActive(false);
    }
    public void ShowMainCanvas()
    {
        mainCanvas.gameObject.SetActive(true);
    }
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
