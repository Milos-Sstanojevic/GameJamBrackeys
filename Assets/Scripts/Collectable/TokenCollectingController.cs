using UnityEngine;

public class TokenCollectingController : MonoBehaviour
{
    [SerializeField] private TokenScriptableObject tokenScriptableObject;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerController>() != null)
        {
            EventManager.Instance.OnCreateDoor(tokenScriptableObject.TokenColor);
            Destroy(gameObject);
        }
    }
}
