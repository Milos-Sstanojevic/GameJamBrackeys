using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    GameObject player;

    [SerializeField] GameObject jumpIndicator;
    [SerializeField] List<Sprite> masks;
    [SerializeField] Vector3 offset;

    private RectTransform jiTransform;
    private Image jiImage;

    [SerializeField] Slider healthBar;
    [SerializeField] TMP_Text keysCountDisplay;
    void Start()
    {
        EventManager.Instance.SubscribeToLeftJumpsChanged(OnJumpCountChanged);
        EventManager.Instance.SubscribeToHealthChanged(OnHealthChanged);
        EventManager.Instance.SubscribeToKeysCountChanged(OnKeysCountChanged);

        jiTransform = jumpIndicator.GetComponent<RectTransform>();
        jiImage = jumpIndicator.GetComponent<Image>();

        player = FindAnyObjectByType<PlayerController>().gameObject;
    }
    private void Update()
    {
        if (player == null)
        {
            player = FindAnyObjectByType<PlayerController>().gameObject;
        }

        jiTransform.position = Camera.main.WorldToScreenPoint(player.transform.position + offset);
    }
    void OnJumpCountChanged(int count)
    {
        if (count <= 4)
        {
            jiImage.sprite = masks[count];
        }
    }
    void OnHealthChanged(int health)
    {
        healthBar.value = health / 100f;
    }
    void OnKeysCountChanged(int count)
    {
        keysCountDisplay.text = count.ToString();
    }
}
