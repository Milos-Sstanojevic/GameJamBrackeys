using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlatformsControlledByLevers : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.GetComponent<DoorsController>())
        {
            EventManager.Instance.OnChangeDirection(this);
        }
    }
}
