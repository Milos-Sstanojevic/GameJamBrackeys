using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalMusic : MonoBehaviour
{
    public static GlobalMusic instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(this);
    }
}
