using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScenarist : MonoBehaviour
{
    [SerializeField] PlayerDoorInventoryHandler inventoryHandler;
    public static TutorialScenarist Instance;
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
    private void OnEnable()
    {
        EventManager.Instance.SubscribeToTutorialsWithoutInventoryFinished(EnableInventory);
    }
    private void OnDisable()
    {
        EventManager.Instance.UnsubscribeToTutorialsWithoutInventoryFinished(EnableInventory);
    }
    void Start()
    {
        CanvasesManager.Instance.HideAllCanvases();
        inventoryHandler.enabled = false;
    }
    public void EnableInventory()
    {
        CanvasesManager.Instance.ShowMainCanvas();
        inventoryHandler.enabled = true;
    }
}
