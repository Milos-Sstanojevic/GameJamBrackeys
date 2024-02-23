using UnityEngine;

public class TokenCollectingController : MonoBehaviour
{
    [SerializeField] private DoorColor doorColor;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            EventManager.Instance.OnCollectToken(doorColor);
            Destroy(gameObject);
        }
    }
}
