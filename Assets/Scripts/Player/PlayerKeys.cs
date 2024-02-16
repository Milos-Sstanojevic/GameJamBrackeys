using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKeys : MonoBehaviour
{
    private int keysCount;
    public int KeysCount
    {
        set
        {
            keysCount = value;
            EventManager.Instance.OnKeysCountChanged(keysCount);
        }
        get { return keysCount; }
    }
    private void Start()
    {
        KeysCount = 0;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            AddKey();
        }
        else if (Input.GetKeyDown(KeyCode.K))
        {
            UseKey();
        }
    }
    public void AddKey()
    {
        KeysCount++;
    }
    public void UseKey()
    {
        KeysCount--;
    }
    public bool HasKeys()
    {
        return KeysCount > 0;
    }
}
