using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour
{
    GameObject player;

    [SerializeField] GameObject jumpIndicator;
    [SerializeField] List<Sprite> masks;
    [SerializeField] Vector3 offset;

    private RectTransform jiTransform;
    private Image jiImage;

    [SerializeField] Slider healthBar;
    [SerializeField] TMP_Text keysCountDisplay;

    private void OnEnable()
    {
        EventManager.Instance.SubscribeToLeftJumpsChanged(OnJumpCountChanged);
        EventManager.Instance.SubscribeToHealthChanged(OnHealthChanged);
        EventManager.Instance.SubscribeToKeysCountChanged(OnKeysCountChanged);
    }
    private void OnDisable()
    {
        EventManager.Instance.UnsubscribeToLeftJumpsChanged(OnJumpCountChanged);
        EventManager.Instance.UnsubscribeToHealthChanged(OnHealthChanged);
        EventManager.Instance.UnsubscribeToKeysCountChanged(OnKeysCountChanged);
    }
    void Start()
    {

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
